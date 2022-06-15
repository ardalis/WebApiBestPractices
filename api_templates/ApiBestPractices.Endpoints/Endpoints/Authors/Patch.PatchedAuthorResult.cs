namespace ApiBestPractices.Endpoints.Endpoints.Authors;

public class PatchedAuthorResult
{
	public string Id { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string? PluralsightUrl { get; set; }
	public string? TwitterAlias { get; set; }
}
