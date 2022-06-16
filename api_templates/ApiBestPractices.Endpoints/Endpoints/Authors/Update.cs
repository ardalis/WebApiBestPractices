using Ardalis.ApiEndpoints;
using Ardalis.RouteAndBodyModelBinding;
using AutoMapper;
using BackendData;
using Microsoft.AspNetCore.Mvc;

namespace ApiBestPractices.Endpoints.Endpoints.Authors;

public class Update : EndpointBaseAsync
		.WithRequest<UpdateAuthorCommand>
		.WithActionResult<UpdatedAuthorResult>
{
	private readonly IAsyncRepository<Author> _repository;
	private readonly IMapper _mapper;

	public Update(IAsyncRepository<Author> repository,
			IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	/// <summary>
	/// Updates an existing Author
	/// </summary>
	[HttpPut("[namespace]/{id}")]
	public override async Task<ActionResult<UpdatedAuthorResult>> HandleAsync([FromRouteAndBody] UpdateAuthorCommand request,
		CancellationToken cancellationToken)
	{
		var author = await _repository.GetByIdAsync(request.Id, cancellationToken);

		if (author is null) return NotFound();

		_mapper.Map(request, author);
		await _repository.UpdateAsync(author, cancellationToken);

		var result = _mapper.Map<UpdatedAuthorResult>(author);
		return result;
	}
}
