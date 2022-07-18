using System.ComponentModel.DataAnnotations;
using BackendData.DataAccess.Config;

namespace ApiBestPractices.Endpoints.Endpoints.Account;

public class LoginUserCommand
{
	[Required]
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	public string Email { get; set; } = string.Empty;

	[Required]
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	public string Password { get; set; } = string.Empty;
}
