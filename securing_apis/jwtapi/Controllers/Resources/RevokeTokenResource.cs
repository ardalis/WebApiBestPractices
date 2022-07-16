using System.ComponentModel.DataAnnotations;

namespace JWTAPI.Controllers.Resources;

public class RevokeTokenResource
{
	[Required]
	public string RefreshToken { get; set; }
	[Required]
	public string EmailAddress { get; set; }
}
