using System;
using System.Linq;
using BackendData.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace APIProjectTests;

public class WebApiApplication : WebApplicationFactory<Program>
{
	private readonly ITestOutputHelper _testOutputHelper;

	public WebApiApplication(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
	}

	protected override IHost CreateHost(IHostBuilder builder)
	{
		builder.UseEnvironment("Testing");

		builder.ConfigureLogging(loggingBuilder =>
		{
			loggingBuilder.Services.AddSingleton<ILoggerProvider>(serviceProvider => new XUnitLoggerProvider(_testOutputHelper));
		});

		builder.ConfigureServices(services =>
		{
			var descriptor = services.SingleOrDefault(
					d => d.ServiceType ==
							 typeof(DbContextOptions<AppDbContext>));

			services.Remove(descriptor);

			string dbName = "InMemoryDbForTesting" + Guid.NewGuid().ToString();

			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseInMemoryDatabase(dbName);
			});

			// Create a new service provider.
			var serviceProvider = services.BuildServiceProvider();

			using (var scope = serviceProvider.CreateScope())
			using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
			{
				try
				{
					appContext.Database.EnsureCreated();
				}
				catch (Exception ex)
				{
					//Log errors or do anything you think is needed
					throw;
				}
			}
		});

		return base.CreateHost(builder);
	}
}
