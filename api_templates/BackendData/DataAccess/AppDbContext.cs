using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BackendData.DataAccess;

public class AppDbContext : DbContext
{
	// Add Migration Script, from Web Project
	// dotnet ef migrations add AddUsers --context AppDbContext -p ../BackendData/BackendData.csproj -s .\ApiBestPractices.Endpoints.csproj -o Migrations
	public AppDbContext()
	{ }

  public AppDbContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<Author> Authors { get; set; } = null!;
	public DbSet<User> Users { get; set; } = null!;

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//{
	//	if (!optionsBuilder.IsConfigured)
	//	{
	//		optionsBuilder.UseSqlite(":memory");
	//	}
	//}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}
