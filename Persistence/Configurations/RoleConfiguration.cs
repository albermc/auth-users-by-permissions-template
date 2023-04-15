using Domain.Entities;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;

namespace Persistence.Configurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
	public void Configure(EntityTypeBuilder<Role> builder)
	{
		builder.ToTable(TablesNames.Roles);

		builder.HasKey(x => x.Id);

		builder.HasMany(x => x.Permissions)
			.WithMany()
			.UsingEntity<RolePermission>();

		builder.HasMany(x => x.Users)
			.WithMany();

		builder.HasData(Role.GetValues());
	}
}
