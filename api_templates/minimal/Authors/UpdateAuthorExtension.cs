using BackendData;
using Swashbuckle.AspNetCore.Annotations;

namespace minimal.Authors;

public static class UpdateAuthorExtension
{
	// use a static extension method to register endpoints
	// reference: http://www.binaryintellect.net/articles/f3dcbb45-fa8b-4e12-b284-f0cd2e5b2dcf.aspx
	public static void RegisterAuthorUpdateEndpoint(this WebApplication app)
	{
		app.MapPut("/authors/{id}",
	[SwaggerOperation(
				Summary = "Updates an author.",
				Description = "Updates an author, which must exist, otherwise NotFound is returned.")]
		[SwaggerResponse(200, "Success")]
		[SwaggerResponse(404, "Not Found")]
		async (IAsyncRepository<Author> repo, int id, AuthorDto updatedAuthor) =>
	{
		if (await repo.GetByIdAsync(id, cancellationToken: default) is Author author)
		{
			author.Name = updatedAuthor.Name;
			author.TwitterAlias = updatedAuthor.TwitterAlias;
			await repo.UpdateAsync(author, cancellationToken: default);
			return Results.Ok();
		}
		else return Results.NotFound();
	})
	 .WithName("UpdateAuthor")
	 .WithTags("AuthorsApi");
	}
}

