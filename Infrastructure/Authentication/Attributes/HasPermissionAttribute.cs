using Microsoft.AspNetCore.Authorization;
using Permission = Domain.Enums.Permission;

namespace Infrastructure.Authentication.Attributes;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
	public HasPermissionAttribute(Permission permission)
		: base(policy: permission.ToString())
	{

	}
}
