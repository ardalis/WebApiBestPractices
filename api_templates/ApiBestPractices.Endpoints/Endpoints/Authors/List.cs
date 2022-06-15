using Ardalis.ApiEndpoints;
using AutoMapper;
using BackendData;
using Microsoft.AspNetCore.Mvc;

namespace ApiBestPractices.Endpoints.Endpoints.Authors;

public class List : EndpointBaseAsync
		.WithoutRequest
		.WithResult<IEnumerable<AuthorListResult>>
{
	private readonly IAsyncRepository<Author> _repository;
	private readonly IMapper _mapper;

	public List(
		IAsyncRepository<Author> repository,
		IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	/// <summary>
	/// List all Authors
	/// </summary>
	[HttpGet("[namespace]")]
	public override async Task<IEnumerable<AuthorListResult>> HandleAsync(CancellationToken cancellationToken = default)
	{
		var result = (await _repository.ListAllAsync(cancellationToken))
				.Select(i => _mapper.Map<AuthorListResult>(i));

		return result;
	}
}
