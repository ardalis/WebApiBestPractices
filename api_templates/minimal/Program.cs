using BackendData;
using BackendData.DataAccess;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Endpoint.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpoints();

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=database.sqlite;Cache=Shared";
builder.Services.AddSqlite<AppDbContext>(connectionString);
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

builder.Services.AddMvcCore(); // pull in default model binders

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsMetadataProviderApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
var app = builder.Build();

await EnsureDb(app.Services, app.Logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
	app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandlingPath = "/error", AllowStatusCode404Response = true });
}
app.UseHttpsRedirection();

app.UseAuthorization();

// for minimal endpoints
app.MapEndpoints();

// Using simple minimal APIs List Authors
app.MapGet("/authors", async (AppDbContext db) => await db.Authors.ToListAsync())
	 .WithName("ListAuthors")
	 .WithTags("AuthorsApi");

app.Run();

async Task EnsureDb(IServiceProvider services, ILogger logger)
{
	using var db = services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
	if (db.Database.IsRelational())
	{
		logger.LogInformation("Ensuring database exists and is up to date at connection string '{connectionString}'", connectionString);
		await db.Database.MigrateAsync();
	}
}
