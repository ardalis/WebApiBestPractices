using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BackendData.DataAccess;

public class AppDbContext : DbContext
{
	public AppDbContext()
	{ }

  public AppDbContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<Author> Authors { get; set; } = null!;

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

