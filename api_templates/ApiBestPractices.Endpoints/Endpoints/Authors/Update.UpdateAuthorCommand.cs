using System.ComponentModel.DataAnnotations;
using BackendData.DataAccess.Config;

namespace ApiBestPractices.Endpoints.Endpoints.Authors;

public class UpdateAuthorCommand
{
	[Required] // From Route
	[System.Text.Json.Serialization.JsonIgnore] // so it doesn't appear in body example schema in Swagger
	// see: https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/1230
	public int Id { get; set; }

	[Required]
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	public string Name { get; set; } = null!;
	[MaxLength(ConfigConstants.DEFAULT_URI_LENGTH)]
	public string? PluralsightUrl { get; set; }
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	public string? TwitterAlias { get; set; }
}
