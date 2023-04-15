using Domain.Entities;
using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Permission = Domain.Enums.Permission;
namespace Persistence.Configurations;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
	public void Configure(EntityTypeBuilder<RolePermission> builder)
	{
		builder.HasKey(x => new { x.RoleId, x.PermissionId });

		builder.HasData(
			Create(Role.Registered, Permission.ReadMember),
			Create(Role.Registered, Permission.AccessMember));
	}

	private static RolePermission Create(Role role, Permission permission)
	{
		return new RolePermission
		{
			RoleId = role.Id,
			PermissionId = (int)permission
		};
	}
}
