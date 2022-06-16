namespace ApiBestPractices.Endpoints.Endpoints.Authors;

public class AuthorDto
{
	public string Id { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string PluralsightUrl { get; set; } = null!;
	public string? TwitterAlias { get; set; }
}
