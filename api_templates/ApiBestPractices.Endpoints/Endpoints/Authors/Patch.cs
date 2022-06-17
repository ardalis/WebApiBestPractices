using Ardalis.ApiEndpoints;
using Ardalis.RouteAndBodyModelBinding;
using AutoMapper;
using BackendData;
using Microsoft.AspNetCore.Mvc;

namespace ApiBestPractices.Endpoints.Endpoints.Authors;

/// <summary>
/// See: https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-6.0
/// </summary>
public class Patch : EndpointBaseAsync
		.WithRequest<PatchAuthorCommand>
		.WithActionResult<PatchedAuthorResult>
{
	private readonly IAsyncRepository<Author> _repository;
	private readonly IMapper _mapper;

	public Patch(IAsyncRepository<Author> repository,
			IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	/// <summary>
	/// Patches an existing Author
	/// </summary>
	[HttpPatch("[namespace]/{id}")]
	public override async Task<ActionResult<PatchedAuthorResult>> HandleAsync([FromRouteAndBody] PatchAuthorCommand request,
		CancellationToken cancellationToken)
	{
		var author = await _repository.GetByIdAsync(request.Id, cancellationToken);
		if (author is null) return NotFound();

		var updatecommand = _mapper.Map<UpdateAuthorCommand>(author);
		request.PatchDocument.ApplyTo(updatecommand);

		// perform model validation of the changes
		TryValidateModel(updatecommand);
		if (!ModelState.IsValid) return BadRequest(ModelState);

		_mapper.Map(updatecommand, author);
		await _repository.UpdateAsync(author, cancellationToken);

		var result = _mapper.Map<PatchedAuthorResult>(author);
		return result;
		// NOTE: Alternately you could return a 204 No Content with a Location Header pointing to /authors/{id}
	}

	/* Examples:
	{
		"patchDocument": [
		{ "op": "replace", "path": "/name", "value": "steve" },
		]
	}
	{
  "patchDocument":  [
        {
            "op": "replace",
            "value": "updated",
            "path":"/twitterAlias"
        }
    ]
}
	*/
}
