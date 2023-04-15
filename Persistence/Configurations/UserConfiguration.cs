using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(u => u.FirstName).HasMaxLength(50);
		builder.Property(u => u.LastName).HasMaxLength(50);
		builder.Property(u => u.Email)
			.HasConversion(
				email => email.Value,
				value => Email.Create(value).Value)
			.HasMaxLength(255);

		builder.HasIndex(u => u.Email).IsUnique();
	}
}
