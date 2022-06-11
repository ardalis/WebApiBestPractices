using apiendpoints.Endpoints.Authors;
using AutoMapper;
using BackendData;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace apiendpoints;

public class AutoMapping : Profile
{
	public AutoMapping()
	{
		CreateMap<CreateAuthorCommand, Author>();
		CreateMap<Author, CreatedAuthorResult>();

		CreateMap<UpdateAuthorCommand, Author>();
		CreateMap<Author, UpdatedAuthorResult>();

		CreateMap<Author, AuthorListResult>();

		CreateMap<Author, AuthorResult>();
	}
}

public class BodyAndRouteBindingSource : BindingSource
{
	public static readonly BindingSource BodyAndRoute = new BodyAndRouteBindingSource(
		"BodyAndRoute",
		"BodyAndRoute",
		true,
		true
		);

	public BodyAndRouteBindingSource(string id, string displayName, bool isGreedy, bool isFromRequest) : base(id, displayName, isGreedy, isFromRequest)
	{
	}

	public override bool CanAcceptDataFrom(BindingSource bindingSource)
	{
		return bindingSource == Body || bindingSource == this;
	}
}

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
public class FromBodyAndRouteAttribute : Attribute, IBindingSourceMetadata
{
	public BindingSource BindingSource => BodyAndRouteBindingSource.BodyAndRoute;
}

public class BodyAndRouteModelBinder : IModelBinder
{
	private readonly IModelBinder _bodyBinder;
	private readonly IModelBinder _complexBinder;

	public BodyAndRouteModelBinder(IModelBinder bodyBinder, IModelBinder complexBinder)
	{
		_bodyBinder = bodyBinder;
		_complexBinder = complexBinder;
	}

	public async Task BindModelAsync(ModelBindingContext bindingContext)
	{
		await _bodyBinder.BindModelAsync(bindingContext);

		if (bindingContext.Result.IsModelSet)
		{
			bindingContext.Model = bindingContext.Result.Model;
		}

		await _complexBinder.BindModelAsync(bindingContext);
	}
}

public class BodyAndRouteModelBinderProvider : IModelBinderProvider
{
	private BodyModelBinderProvider _bodyModelBinderProvider;
	private ComplexObjectModelBinderProvider _complexTypeModelBinderProvider;

	public BodyAndRouteModelBinderProvider(BodyModelBinderProvider bodyModelBinderProvider, ComplexObjectModelBinderProvider complexTypeModelBinderProvider)
	{
		_bodyModelBinderProvider = bodyModelBinderProvider;
		_complexTypeModelBinderProvider = complexTypeModelBinderProvider;
	}

	public IModelBinder GetBinder(ModelBinderProviderContext context)
	{
		var bodyBinder = _bodyModelBinderProvider.GetBinder(context);
		var complexBinder = _complexTypeModelBinderProvider.GetBinder(context);

		if (context.BindingInfo.BindingSource != null
			&& context.BindingInfo.BindingSource.CanAcceptDataFrom(BodyAndRouteBindingSource.BodyAndRoute))
		{
			return new BodyAndRouteModelBinder(bodyBinder, complexBinder);
		}
		else
		{
			return null;
		}
	}
}

public static class BodyAndRouteModelBinderProviderSetup
{
	public static void InsertBodyAndRouteBinding(this IList<IModelBinderProvider> providers)
	{
		var bodyProvider = providers.Single(provider => provider.GetType() == typeof(BodyModelBinderProvider)) as BodyModelBinderProvider;
		var complexProvider = providers.Single(provider => provider.GetType() == typeof(ComplexObjectModelBinderProvider)) as ComplexObjectModelBinderProvider;

		var bodyAndRouteProvider = new BodyAndRouteModelBinderProvider(bodyProvider, complexProvider);

		providers.Insert(0, bodyAndRouteProvider);
	}
}

// Converts any custom BindingSource that accepts Body into RequestBody parameters in Swagger
// See: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1002#issuecomment-760002223
public class CustomFromBodyOperationFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		var apiBodyParameter =
				context.ApiDescription.ParameterDescriptions.FirstOrDefault(p =>
						p.Source.CanAcceptDataFrom(BindingSource.Body));

		if (apiBodyParameter == null) return;

		var swaggerQueryParameter = operation.Parameters
				.FirstOrDefault(p => p.Name == apiBodyParameter.Name && p.In == ParameterLocation.Query);

		if (swaggerQueryParameter == null) return;

		operation.Parameters.Remove(swaggerQueryParameter);
		operation.RequestBody = new OpenApiRequestBody
		{
			Content = { ["application/json"] = new OpenApiMediaType { Schema = swaggerQueryParameter.Schema } }
		};
	}
}
