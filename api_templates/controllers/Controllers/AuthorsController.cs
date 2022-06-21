using BackendData;
using controllers.ApiModels;
using Microsoft.AspNetCore.Mvc;

namespace controllers.Controllers;


[ApiController]
[Route("Authors")]
public class AuthorsController : ControllerBase
{
	private readonly ILogger<AuthorsController> _logger;
	private readonly IAsyncRepository<Author> _authorRepository;

	public AuthorsController(ILogger<AuthorsController> logger,
		IAsyncRepository<Author> authorRepository)
	{
		_logger = logger;
		_authorRepository = authorRepository;
	}

	[HttpGet]
	[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
	// NOTE: VaryByQueryKeys requires adding Response Cache Middleware (and services)
	public async Task<ActionResult<List<AuthorDto>>> Index(CancellationToken cancellationToken)
	{
		_logger.LogInformation("Returning Author List from /Authors.");

		var authors = await _authorRepository.ListAllAsync(cancellationToken);

		var responseModel = authors
			.Select(a => new AuthorDto(a.Id, a.Name, a.TwitterAlias ?? ""))
			.ToList();

		return Ok(responseModel);
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorDto))]
	public async Task<ActionResult<AuthorDto>> Create(AuthorDto newAuthor,
		CancellationToken cancellationToken)
	{
		var author = new Author
		{
			Name = newAuthor.Name,
			TwitterAlias = newAuthor.TwitterAlias
		};

		await _authorRepository.AddAsync(author, cancellationToken);

		var authorDto = new AuthorDto(author.Id, author.Name, author.TwitterAlias);
		return Created($"authors/{author.Id}", authorDto);
	}
}
