using System.Linq;
using BackendData.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace APIProjectTests;

class WebApiApplication : WebApplicationFactory<Program>
{
	private readonly ITestOutputHelper _testOutputHelper;
	public SqliteConnection SqliteConnection { get; set; }

	public WebApiApplication(ITestOutputHelper testOutputHelper)
	{
		_testOutputHelper = testOutputHelper;
		SqliteConnection = new SqliteConnection("DataSource=:memory:");
	}

	protected override IHost CreateHost(IHostBuilder builder)
	{
		builder.UseEnvironment("Testing");

		builder.ConfigureLogging(loggingBuilder =>
		{
			loggingBuilder.Services.AddSingleton<ILoggerProvider>(serviceProvider => new XUnitLoggerProvider(_testOutputHelper));
		});

		//builder.ConfigureServices(services =>
		//{
		//	var descriptor = services.SingleOrDefault(
		//			d => d.ServiceType ==
		//					 typeof(DbContextOptions<AppDbContext>));

		//	services.Remove(descriptor);

		//	// Create a new service provider.
		//	var serviceProvider = new ServiceCollection()
		//			.AddEntityFrameworkSqlite()
		//			.BuildServiceProvider();

		//	// Add a database context (AppDbContext) using an in-memory database for testing.
		//	services.AddDbContext<AppDbContext>(options =>
		//	{
		//		options.UseSqlite(this.SqliteConnection);
		//		options.UseInternalServiceProvider(serviceProvider);
		//	});

		//	// Build the service provider.
		//	var sp = services.BuildServiceProvider();

		//	// Create a scope to obtain a reference to the database contexts
		//	var scope = sp.CreateScope();
		//	var scopedServices = scope.ServiceProvider;
		//	var appDb = scopedServices.GetRequiredService<AppDbContext>();
		//	appDb.Database.EnsureCreated();

		//	//Context = appDb;
		//});

		//// Add mock/test services to the builder here
		builder.ConfigureServices(services =>
		{
			services.AddScoped(sp =>
			{
				// Replace SQLite with in-memory database for tests
				return new DbContextOptionsBuilder<AppDbContext>()
				.UseSqlite("DataSource=:memory:")
				.UseApplicationServiceProvider(sp)
				.Options;
			});
		});

		return base.CreateHost(builder);
	}
}
