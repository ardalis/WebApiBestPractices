using BackendData;
using FastEndpoints;

namespace FastEndpointsAPI.Authors;

public class Create : Endpoint<Create.AuthorRequest, Create.AuthorResponse, Create.AuthorMapper>
{
	private readonly IAsyncRepository<Author> _authorRepository;

	public Create(IAsyncRepository<Author> authorRepository)
	{
		_authorRepository = authorRepository;
	}

	public override void Configure()
	{
		Verbs(Http.POST);
		Routes("/authors");
		AllowAnonymous();
	}

	public override async Task HandleAsync(AuthorRequest req, CancellationToken cancellationToken)
	{
		var author = Map.ToEntity(req);

		await _authorRepository.AddAsync(author, cancellationToken);

		await SendAsync(new AuthorResponse()
		{
				Id = author.Id,
				Name = author.Name,
				TwitterAlias = author.TwitterAlias
		});
	}

	public class AuthorRequest
	{
		public string Name { get; set; }
		public string TwitterAlias { get; set; }
	}
	public class AuthorResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string TwitterAlias { get; set; }
	}

	public class AuthorMapper : Mapper<AuthorRequest, AuthorResponse, Author>
	{

		public override AuthorResponse FromEntity(Author e)
		{
			return new AuthorResponse()
			{
				Id = e.Id,
				Name = e.Name,
				TwitterAlias = e.TwitterAlias
			};
		}

		public override Author ToEntity(AuthorRequest r)
		{
			return new Author
			{
				Name = r.Name,
				TwitterAlias = r.TwitterAlias
			};

		}
	}
}
