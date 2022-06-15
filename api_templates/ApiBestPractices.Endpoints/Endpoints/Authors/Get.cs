using Ardalis.ApiEndpoints;
using AutoMapper;
using BackendData;
using Microsoft.AspNetCore.Mvc;

namespace ApiBestPractices.Endpoints.Endpoints.Authors;

public class Get : EndpointBaseAsync
			.WithRequest<int>
			.WithActionResult<AuthorDto>
{
	private readonly IAsyncRepository<Author> _repository;
	private readonly IMapper _mapper;

	public Get(IAsyncRepository<Author> repository,
			IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	/// <summary>
	/// Get a specific Author
	/// </summary>
	[HttpGet("[namespace]/{id}", Name = "[namespace]_[controller]")]
	public override async Task<ActionResult<AuthorDto>> HandleAsync(int id, CancellationToken cancellationToken)
	{
		var author = await _repository.GetByIdAsync(id, cancellationToken);

		if (author is null) return NotFound();

		var result = _mapper.Map<AuthorDto>(author);

		return result;
	}
}
