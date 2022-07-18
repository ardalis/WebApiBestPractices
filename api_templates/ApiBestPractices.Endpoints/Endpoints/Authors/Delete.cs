using Ardalis.ApiEndpoints;
using BackendData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBestPractices.Endpoints.Endpoints.Authors;

[Authorize]
public class Delete : EndpointBaseAsync
		.WithRequest<int>
		.WithActionResult
{
	private readonly IAsyncRepository<Author> _repository;

	public Delete(IAsyncRepository<Author> repository)
	{
		_repository = repository;
	}

	/// <summary>
	/// Deletes an Author
	/// </summary>
	[HttpDelete("[namespace]/{id}")]
	public override async Task<ActionResult> HandleAsync(int id, CancellationToken cancellationToken)
	{
		var author = await _repository.GetByIdAsync(id, cancellationToken);

		if (author is null)
		{
			return NotFound(id);
		}

		await _repository.DeleteAsync(author, cancellationToken);

		// see https://restfulapi.net/http-methods/#delete
		return NoContent();
	}
}
