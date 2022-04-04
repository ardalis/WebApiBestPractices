using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendData.DataAccess.Config;

public class AuthorConfig : IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.Property(e => e.Name)
				.IsRequired();

		builder.HasData(SeedData.Authors());
	}
}
