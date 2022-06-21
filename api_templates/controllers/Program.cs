using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BackendData;
using BackendData.DataAccess;
using controllers.Controllers;
using controllers.Interfaces;
using controllers.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=database.sqlite;Cache=Shared";
builder.Services.AddSqlite<AppDbContext>(connectionString);
builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

builder.Services.AddResponseCaching();

builder.Services.AddControllers(options =>
{
	options.RespectBrowserAcceptHeader = true;
}).AddXmlSerializerFormatters()
.AddControllersAsServices();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Controller-based API Sample", Version = "v1" });
});

// add application services
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddMediatR(typeof(IAuthorService));

// configure autofac - necessary for MediatR property on base controller class example
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
	// configure all controllers that inherit from MyBaseApiController to have properties injected
	containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
		.Where(t => t.IsSubclassOf(typeof(MyBaseApiController)))
		.PropertiesAutowired();
		});

var app = builder.Build();

await EnsureDb(app.Services, app.Logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/error-development");
  app.UseSwagger();
  app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/error");
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseResponseCaching();

app.UseAuthorization(); 

app.UseEndpoints(endpoints => endpoints.MapControllers());

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
