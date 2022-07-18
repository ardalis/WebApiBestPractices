using JWTAPI.Core.Repositories;
using JWTAPI.Core.Security.Hashing;
using JWTAPI.Core.Security.Tokens;
using JWTAPI.Core.Services;
using JWTAPI.Extensions;
using JWTAPI.Persistence;
using JWTAPI.Security.Hashing;
using JWTAPI.Security.Tokens;
using JWTAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace JWTAPI;

/// <summary>
/// See: https://github.com/ardalis/jwt-api forked from
/// https://github.com/evgomes/jwt-api
/// </summary>
public class Startup
{
	public IConfiguration Configuration { get; }

	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public void ConfigureServices(IServiceCollection services)
	{
		services.AddDbContext<AppDbContext>(options =>
		{
			options.UseInMemoryDatabase("jwtapi");
		});

		services.AddControllers();

		services.AddCustomSwagger();

		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.AddSingleton<IPasswordHasher, PasswordHasher>();
		services.AddSingleton<ITokenHandler, TokenHandler>();

		services.AddScoped<IUserService, UserService>();
		services.AddScoped<IAuthenticationService, AuthenticationService>();

		services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
		var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

		var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
		services.AddSingleton(signingConfigurations);

		services.ConfigureJwtAuthentication(tokenOptions, signingConfigurations);

		services.AddAutoMapper(this.GetType().Assembly);
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		app.UseDeveloperExceptionPage();

		app.UseRouting();

		app.UseCustomSwagger();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapControllers();
		});
	}
}
