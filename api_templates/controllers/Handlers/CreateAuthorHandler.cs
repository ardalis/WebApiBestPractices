using controllers.ApiModels;
using controllers.Interfaces;
using MediatR;

namespace controllers.Handlers;

public class CreateAuthorCommand : IRequest<AuthorDto>
{
	public CreateAuthorCommand(string name, string twitterAlias)
	{
		Name = name;
		TwitterAlias = twitterAlias;
	}

	public string Name { get; }
	public string TwitterAlias { get; }
}
public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, AuthorDto>
{
	private readonly IAuthorService _authorService;

	public CreateAuthorHandler(IAuthorService authorService)
	{
		_authorService = authorService;
	}

	public async Task<AuthorDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
	{
		var authorDto = new AuthorDto() { Name = request.Name, TwitterAlias = request.TwitterAlias };
		return await _authorService.CreateAndSave(authorDto, cancellationToken);
	}
}
