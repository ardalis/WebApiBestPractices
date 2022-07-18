using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JWTAPI.Core.Models;
using JWTAPI.Core.Security.Hashing;
using JWTAPI.Core.Security.Tokens;
using Microsoft.Extensions.Options;

namespace JWTAPI.Security.Tokens;

public class TokenHandler : ITokenHandler
{
	private readonly ISet<RefreshTokenWithEmail> _refreshTokens = new HashSet<RefreshTokenWithEmail>();

	private readonly TokenOptions _tokenOptions;
	private readonly SigningConfigurations _signingConfigurations;
	private readonly IPasswordHasher _passwordHasher;

	public TokenHandler(IOptions<TokenOptions> tokenOptionsSnapshot,
		SigningConfigurations signingConfigurations,
		IPasswordHasher passwordHasher)
	{
		_passwordHasher = passwordHasher;
		_tokenOptions = tokenOptionsSnapshot.Value;
		_signingConfigurations = signingConfigurations;
	}

	public AccessToken CreateAccessToken(User user)
	{
		var refreshToken = BuildRefreshToken();
		var accessToken = BuildAccessToken(user, refreshToken);
		_refreshTokens.Add(new RefreshTokenWithEmail
		{
			Email = user.Email,
			RefreshToken = refreshToken
		});

		return accessToken;
	}

	public RefreshToken TakeRefreshToken(string token, string email)
	{
		if (string.IsNullOrWhiteSpace(token))
			return null;
		if (string.IsNullOrWhiteSpace(email))
			return null;

		var refreshTokenWithEmail = _refreshTokens
			.SingleOrDefault(rt => rt.RefreshToken.Token == token &&
														 rt.Email == email);

		if (refreshTokenWithEmail == null) return null;

		_refreshTokens.Remove(refreshTokenWithEmail);

		return refreshTokenWithEmail.RefreshToken;
	}

	public void RevokeRefreshToken(string token, string email)
	{
		TakeRefreshToken(token, email);
	}

	private RefreshToken BuildRefreshToken()
	{
		var refreshToken = new RefreshToken
		(
				token: _passwordHasher.HashPassword(Guid.NewGuid().ToString()),
				expiration: DateTime.UtcNow.AddSeconds(_tokenOptions.RefreshTokenExpiration).Ticks
		);

		return refreshToken;
	}

	private class RefreshTokenWithEmail
	{
		public string Email { get; set; }
		public RefreshToken RefreshToken { get; set; }
	}

	private AccessToken BuildAccessToken(User user, RefreshToken refreshToken)
	{
		var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

		var securityToken = new JwtSecurityToken
		(
				issuer: _tokenOptions.Issuer,
				audience: _tokenOptions.Audience,
				claims: GetClaims(user),
				expires: accessTokenExpiration,
				notBefore: DateTime.UtcNow,
				signingCredentials: _signingConfigurations.SigningCredentials
		);

		var handler = new JwtSecurityTokenHandler();
		var accessToken = handler.WriteToken(securityToken);

		return new AccessToken(accessToken, accessTokenExpiration.Ticks, refreshToken);
	}

	private IEnumerable<Claim> GetClaims(User user)
	{
		var claims = new List<Claim>
					{
							new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
							new Claim(JwtRegisteredClaimNames.Sub, user.Email)
					};

		foreach (var userRole in user.UserRoles)
		{
			claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Name));
		}

		return claims;
	}
}
