using System.ComponentModel.DataAnnotations;
using BackendData;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace apiendpoints.Endpoints.Authors;

public class PatchAuthorCommand
{
	[Required] // From Route
	public int Id { get; set; }

	[Required]
	public JsonPatchDocument<AuthorDto> PatchDocument { get; set; }
}

public class AuthorDto
{
	public string Name { get; set; } = "New Author";
	public string? PluralsightUrl { get; set; }
	public string? TwitterAlias { get; set; }
}
