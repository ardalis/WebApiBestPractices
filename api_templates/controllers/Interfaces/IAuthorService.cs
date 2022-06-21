using controllers.ApiModels;

namespace controllers.Interfaces;

public interface IAuthorService
{
	Task<AuthorDto> CreateAndSave(AuthorDto newAuthor, CancellationToken cancellationToken);
}
