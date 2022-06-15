using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendData.DataAccess.Config;

public class AuthorConfig : IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.Property(a => a.Name)
				.IsRequired()
				.HasMaxLength(ConfigConstants.DEFAULT_NAME_LENGTH);

		builder.Property(a => a.PluralsightUrl)
			.HasMaxLength(ConfigConstants.DEFAULT_URI_LENGTH);

		builder.Property(a => a.TwitterAlias)
			.HasMaxLength(ConfigConstants.DEFAULT_NAME_LENGTH);

		builder.HasData(SeedData.Authors());
	}
}
