using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;

namespace apiendpoints.Endpoints.Authors;

public class List : EndpointBaseAsync
		.WithoutRequest
		.WithResult<IEnumerable<AuthorListResult>>
{
	//private readonly IAsyncRepository<Author> repository;
	//private readonly IMapper mapper;

	public List()
	//IAsyncRepository<Author> repository,
	//IMapper mapper)
	{
		//this.repository = repository;
		//this.mapper = mapper;
	}

	/// <summary>
	/// List all Authors
	/// </summary>
	[HttpGet("api/[namespace]")]
	public override async Task<IEnumerable<AuthorListResult>> HandleAsync(CancellationToken cancellationToken = default)
	{
		return new List<AuthorListResult>();
	}
}