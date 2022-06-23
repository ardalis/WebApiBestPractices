using controllers.ApiModels;
using controllers.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers;

// SHOW USING MEDIATR FROM BASE INSTEAD OF A REPOSITORY OR SERVICE

[ApiController]
public abstract class MyBaseApiController : ControllerBase
{
	// this requires a third party DI tool like Autofac and adding .AddControllersAsServices in Program.cs
	public IMediator Mediator { get; set; }
}

[Route("AuthorsUsingMediatrFromBase")]
public class AuthorsUsingMediatrFromBaseController : MyBaseApiController
{
	[HttpPost()]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorDto))]
	public async Task<ActionResult<AuthorDto>> Create(CreateAuthorCommand newAuthorCommand,
		CancellationToken cancellationToken)
	{
		//var createAuthorCommand = new CreateAuthorCommand(newAuthor.Name, newAuthor.TwitterAlias);
		var createdAuthorDto = await Mediator.Send(newAuthorCommand, cancellationToken);
		return Created($"authors/{createdAuthorDto.Id}", createdAuthorDto);
	}
}
