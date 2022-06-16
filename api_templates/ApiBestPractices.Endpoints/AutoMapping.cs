using ApiBestPractices.Endpoints.Endpoints.Authors;
using AutoMapper;
using BackendData;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiBestPractices.Endpoints;

public class AutoMapping : Profile
{
	public AutoMapping()
	{
		CreateMap<CreateAuthorCommand, Author>();
		CreateMap<Author, CreatedAuthorResult>();

		CreateMap<UpdateAuthorCommand, Author>();
		CreateMap<Author, UpdatedAuthorResult>();

		CreateMap<Author, AuthorListResult>();

		CreateMap<AuthorDto, Author>().ReverseMap();
		CreateMap<Author, PatchedAuthorResult>();
	}
}
