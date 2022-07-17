using ApiBestPractices.Endpoints;
using ApiBestPractices.Endpoints.Endpoints.Authors;
using Ardalis.RouteAndBodyModelBinding;
using BackendData;
using BackendData.DataAccess;
using BackendData.Security;
using BackendData.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=database.sqlite;Cache=Shared";
var connection = new SqliteConnection(connectionString);
connection.Open();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=database.sqlite;Cache=Shared";
//builder.Services.AddSqlite<AppDbContext>(connectionString);


builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseSqlite(connection)); // will be created in web project root

builder.Services.AddControllers(options =>
	{
		options.UseNamespaceRouteToken();
		options.ModelBinderProviders.InsertRouteAndBodyBinding();
	})
	.AddNewtonsoftJson(); // needed for JsonPatch support
builder.Services.AddAutoMapper(typeof(List));
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenFactory, TokenFactory>();

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
builder.Services.AddSingleton(signingConfigurations);

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Endpoints", Version = "v1" });
	c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "ApiBestPractices.Endpoints.xml"));
	c.UseApiEndpoints();
	c.OperationFilter<RouteAndBodyOperationFilter>();
	//c.DocumentFilter<JsonPatchDocumentFilter>();
});

builder.Services.AddHostedService<DataConsistencyWorker>();
builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();

var app = builder.Build();

await EnsureDb(app.Services, app.Logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Endpoints");
		c.RoutePrefix = "";
	});
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	//app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(app => app.MapControllers());

await using var scope = app.Services.CreateAsyncScope();
using var db = scope.ServiceProvider.GetService<AppDbContext>();

await EnsureDb(app.Services, app.Logger);

app.Run();

async Task EnsureDb(IServiceProvider services, ILogger logger)
{
	using var db = services.CreateScope()
		.ServiceProvider.GetRequiredService<AppDbContext>();
	if (db.Database.IsRelational())
	{
		logger.LogInformation("Ensuring database exists and is up to date at connection string '{connectionString}'", connectionString);
		//await db.Database.EnsureCreatedAsync();
		await db.Database.MigrateAsync();
	}
}

// Make the implicit Program class public so test projects can access it
public partial class Program { }
