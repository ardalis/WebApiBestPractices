using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace BackendData.Security;


public class TokenFactory : ITokenFactory
{
	private readonly TokenOptions _tokenOptions;
	private readonly SigningConfigurations _signingConfigurations;

	public TokenFactory(IOptions<TokenOptions> tokenOptionsSnapshot,
		SigningConfigurations signingConfigurations)
	{
		_tokenOptions = tokenOptionsSnapshot.Value;
		_signingConfigurations = signingConfigurations;
	}
	public AccessToken CreateAccessToken(User user)
	{
		return BuildAccessToken(user);
	}

	private AccessToken BuildAccessToken(User user)
	{
		var tokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

		var securityToken = new JwtSecurityToken
		(
				issuer: _tokenOptions.Issuer,
				audience: _tokenOptions.Audience,
				claims: GetClaims(user),
				expires: tokenExpiration,
				notBefore: DateTime.UtcNow,
				signingCredentials: _signingConfigurations.SigningCredentials
		);

		var handler = new JwtSecurityTokenHandler();
		var accessToken = handler.WriteToken(securityToken);

		return new AccessToken(accessToken, tokenExpiration.Ticks);
	}

	private IEnumerable<Claim> GetClaims(User user)
	{
		var claims = new List<Claim>
					{
							new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
							new Claim(JwtRegisteredClaimNames.Sub, user.Email)
					};

		//foreach (var userRole in user.UserRoles)
		//{
		//	claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
		//}

		return claims;
	}
}
