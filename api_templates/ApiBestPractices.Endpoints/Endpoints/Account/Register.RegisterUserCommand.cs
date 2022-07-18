using System.ComponentModel.DataAnnotations;
using BackendData.DataAccess.Config;

namespace ApiBestPractices.Endpoints.Endpoints.Account;

public class RegisterUserCommand
{
	[Required]
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	public string Email { get; set; }

	[Required]
	[MaxLength(ConfigConstants.DEFAULT_NAME_LENGTH)]
	public string Password { get; set; }
}
