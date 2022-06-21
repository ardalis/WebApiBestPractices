using BackendData;
using controllers.ApiModels;
using controllers.Interfaces;

namespace controllers.Services;

public class AuthorService : IAuthorService
{
	private readonly ILogger<AuthorService> _logger;
	private readonly IAsyncRepository<Author> _authorRepository;

	public AuthorService(ILogger<AuthorService> logger,
		IAsyncRepository<Author> authorRepository)
	{
		_logger = logger;
		_authorRepository = authorRepository;
	}

	public async Task<AuthorDto> CreateAndSave(AuthorDto newAuthor, CancellationToken cancellationToken)
	{
		var author = new Author
		{
			Name = newAuthor.Name,
			TwitterAlias = newAuthor.TwitterAlias
		};

		await _authorRepository.AddAsync(author, cancellationToken);

		return new AuthorDto(author.Id, author.Name, author.TwitterAlias);
	}
}
