using JWTAPI.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JWTAPI;

public static class AuthExtensions
{
	public static void ConfigureJwtAuthentication(this IServiceCollection services,
		TokenOptions tokenOptions,
		SigningConfigurations signingConfigurations)
	{
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(jwtBearerOptions =>
			{
				jwtBearerOptions.TokenValidationParameters =
					new TokenValidationParameters()
					{
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = tokenOptions.Issuer,
						ValidAudience = tokenOptions.Audience,
						IssuerSigningKey = signingConfigurations.SecurityKey,
						ClockSkew = TimeSpan.Zero
					};
			});
	}
}
