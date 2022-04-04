using System.ComponentModel.DataAnnotations;

namespace apiendpoints.Endpoints.Authors;

public class UpdateAuthorCommand
{
	[Required]
	public int Id { get; set; }
	[Required]
	public string Name { get; set; } = null!;
}
