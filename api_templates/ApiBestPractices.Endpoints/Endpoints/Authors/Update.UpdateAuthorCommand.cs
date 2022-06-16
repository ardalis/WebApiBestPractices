using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ApiBestPractices.Endpoints.Endpoints.Authors;

public class UpdateAuthorCommand
{
	[Required] // From Route
	[System.Text.Json.Serialization.JsonIgnore] // so it doesn't appear in body example schema in Swagger
	// see: https://github.com/domaindrivendev/Swashbuckle.WebApi/issues/1230
	public int Id { get; set; }

	[Required]
	public string Name { get; set; } = null!;
	public string? PluralsightUrl { get; set; }
	public string? TwitterAlias { get; set; }
}
