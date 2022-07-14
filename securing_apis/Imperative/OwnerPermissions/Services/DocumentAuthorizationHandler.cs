using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using OwnerPermissions.Models;

namespace OwnerPermissions.Services;

public class DocumentAuthorizationHandler :
				AuthorizationHandler<SameAuthorRequirement, Document>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
																								 SameAuthorRequirement requirement,
																								 Document resource)
	{
		if (context.User.Identity?.Name == resource.Author)
		{
			context.Succeed(requirement);
		}

		return Task.CompletedTask;
	}
}

public class SameAuthorRequirement : IAuthorizationRequirement { }

public class DocumentAuthorizationCrudHandler :
				AuthorizationHandler<OperationAuthorizationRequirement, Document>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
																								 OperationAuthorizationRequirement requirement,
																								 Document resource)
	{
		if (context.User.Identity?.Name == resource.Author &&
				requirement.Name == Operations.Read.Name)
		{
			context.Succeed(requirement);
		}

		return Task.CompletedTask;
	}
}

public static class Operations
{
	public static OperationAuthorizationRequirement Create =
			new OperationAuthorizationRequirement { Name = nameof(Create) };
	public static OperationAuthorizationRequirement Read =
			new OperationAuthorizationRequirement { Name = nameof(Read) };
	public static OperationAuthorizationRequirement Update =
			new OperationAuthorizationRequirement { Name = nameof(Update) };
	public static OperationAuthorizationRequirement Delete =
			new OperationAuthorizationRequirement { Name = nameof(Delete) };
}
