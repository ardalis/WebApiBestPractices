using System.ComponentModel.DataAnnotations;

namespace apiendpoints.Endpoints.Authors;

public class CreateAuthorCommand
{
	[Required]
	public string Name { get; set; } = null!;
	public string PluralsightUrl { get; set; } = null!;
	public string? TwitterAlias { get; set; }
}


