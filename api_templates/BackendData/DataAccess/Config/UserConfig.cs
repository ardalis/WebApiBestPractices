using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendData.DataAccess.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.Property(x => x.Email)
				.IsRequired()
				.HasMaxLength(ConfigConstants.DEFAULT_NAME_LENGTH);

		builder.Property(x => x.Password)
			.HasMaxLength(ConfigConstants.DEFAULT_NAME_LENGTH);

		builder.HasData(SeedData.Users());
	}
}
