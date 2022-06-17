using BackendData;
using controllers.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers;

[Route("Authors")]
public class AuthorsController : ControllerBase
{
	[HttpGet]
	public ActionResult<List<AuthorDto>> Index()
	{
		return new List<AuthorDto>();
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created, Type=typeof(AuthorDto))]
	public IActionResult Create(AuthorDto newAuthor)
	{
		var author = new Author { Name = newAuthor.Name, 
			TwitterAlias = newAuthor.TwitterAlias };

		// Add to db

		var authorDto = new AuthorDto(author.Id, author.Name, author.TwitterAlias);
		return Created("authors/{id}", authorDto);
	}
}
