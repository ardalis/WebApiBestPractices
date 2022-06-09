using BackendData;
using BackendData.DataAccess;
using FastEndpoints;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=database.sqlite;Cache=Shared";
var connection = new SqliteConnection(connectionString);
connection.Open();

// Add services to the container.
builder.Services.AddFastEndpoints();
builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseSqlite(connection)); // will be created in web project root
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseFastEndpoints();

using (var serviceScope = app.Services.CreateScope())
{
	var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
	context.Database.EnsureCreated();
}

app.Run();
