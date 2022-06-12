﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace apiendpoints.Endpoints.Authors;

public class UpdateAuthorCommand
{
	[Required] // From Route
	public int Id { get; set; }

	[Required]
	public string Name { get; set; } = null!;
	public string? PluralsightUrl { get; set; }
	public string? TwitterAlias { get; set; }
}
