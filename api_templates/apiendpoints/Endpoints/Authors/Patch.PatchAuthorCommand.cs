using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.JsonPatch;

namespace apiendpoints.Endpoints.Authors;

public class PatchAuthorCommand
{
	[Required] // From Route
	[System.Text.Json.Serialization.JsonIgnore] // so it doesn't appear in body example schema in Swagger
	public int Id { get; set; }

	[Required]
	public JsonPatchDocument<AuthorDto> PatchDocument { get; set; }
}
