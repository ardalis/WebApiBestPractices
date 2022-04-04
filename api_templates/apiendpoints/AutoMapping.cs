using apiendpoints.Endpoints.Authors;
using AutoMapper;
using BackendData;

namespace apiendpoints;

public class AutoMapping : Profile
{
	public AutoMapping()
	{
		//CreateMap<CreateAuthorCommand, Author>();
		//CreateMap<UpdateAuthorCommand, Author>();

		//CreateMap<Author, CreateAuthorResult>();
		//CreateMap<Author, UpdatedAuthorResult>();
		CreateMap<Author, AuthorListResult>();
		//CreateMap<Author, AuthorResult>();
	}
}
