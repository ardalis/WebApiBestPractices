using System.ComponentModel.DataAnnotations;
using BackendData.DataAccess.Config;

namespace ApiBestPractices.Endpoints.Endpoints.Authors;

public class CreateAuthorCommand
{
	[Required]
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	public string Name { get; set; } = null!;
	[MaxLength(ConfigConstants.DEFAULT_URI_LENGTH)]
	public string PluralsightUrl { get; set; } = null!;
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	[RegularExpression("^@(\\w){1,15}$")]
	public string? TwitterAlias { get; set; }
}

// You can use a record type, which still supports Data Annotations
public record CreateAuthorRequest(
	[Required]
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	string Name,
	[MaxLength(ConfigConstants.DEFAULT_URI_LENGTH)]
	string PluralsightUrl,
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	string? TwitterAlias
	);
