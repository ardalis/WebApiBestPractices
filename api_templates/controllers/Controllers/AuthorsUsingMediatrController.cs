using controllers.ApiModels;
using controllers.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers;

// SHOW USING MEDIATR INSTEAD OF A REPOSITORY OR SERVICE

[ApiController]
[Route("AuthorsUsingMediatr")]
public class AuthorsUsingMediatrController : ControllerBase
{
	public AuthorsUsingMediatrController(IMediator mediator)
	{
		Mediator = mediator;
	}

	public IMediator Mediator { get; }

	[HttpPost()]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorDto))]
	public async Task<ActionResult<AuthorDto>> Create(AuthorDto newAuthor,
		CancellationToken cancellationToken)
	{
		var createAuthorCommand = new CreateAuthorCommand(newAuthor.Name, newAuthor.TwitterAlias);
		var createdAuthorDto = await Mediator.Send(createAuthorCommand, cancellationToken);
		return Created($"authors/{createdAuthorDto.Id}", createdAuthorDto);
	}
}
