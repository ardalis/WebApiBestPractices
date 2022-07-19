using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ApiBestPractices.Endpoints.Endpoints.Authors;
using BackendData.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace APIProjectTests.Endpoints;

// Do everything in the test method
// Works, but results in a lot of duplication when you have more than one test
public class GetById_EndpointAllInOne
{
	private readonly ITestOutputHelper _testOutputHelper;
	private readonly ILogger _logger;
	private readonly int _testAuthorId = 1;

	public GetById_EndpointAllInOne(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
		_logger = XUnitLogger.CreateLogger<Swagger>(_testOutputHelper);
	}

	[Fact]
	public async Task ReturnsSeededAuthorWithId1()
	{
		// Arrange
		var application = new WebApplicationFactory<Program>()
			.WithWebHostBuilder(builder =>
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

					var serviceProvider = services.BuildServiceProvider();

					using (var scope = serviceProvider.CreateScope())
					using (var appContext = scope.ServiceProvider.GetRequiredService<AppDbContext>())
					{
						try
						{
							// this will call the Seed method defined in AuthorConfig.cs in BackendData project
							appContext.Database.EnsureCreated();
						}
						catch (Exception ex)
						{
							//Log errors or do anything you think is needed
							throw;
						}
					}
				});
			});

		var client = application.CreateClient();

		// Act
		var response = await client.GetAsync(Routes.Authors.Get(_testAuthorId));
		response.EnsureSuccessStatusCode();
		var stringResult = await response.Content.ReadAsStringAsync();
		_logger.LogInformation(stringResult);
		var result = JsonSerializer.Deserialize<AuthorDto>(stringResult, JsonOptionConstants.SerializerOptions);

		// Assert
		Assert.NotNull(result);
		Assert.Equal("Steve Smith", result.Name);
	}
}
