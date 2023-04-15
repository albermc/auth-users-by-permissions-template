using Application.Users.GetUserById;
using Application.Users.Login;
using Domain.Entities.Users;
using Domain.Shared;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Permission = Domain.Enums.Permission;

namespace Presentation.Controllers;

[Route("api/users")]
//[HasPermission(Permission.AccessMember)]
public sealed class UsersController : ApiController
{
	public UsersController(ISender sender) : base(sender)
	{
	}

	[HasPermission(Permission.ReadMember)]
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellation)
	{
		var query = new GetUserByIdQuery(id);

		Result<User> userResult = await Sender.Send(query, cancellation);

		if (userResult.IsFailure)
		{
			return BadRequest(userResult.Errors);
		}

		return Ok(userResult.Value);
	}

	[HttpPost]
	public async Task<IActionResult> LoginUser([FromBody] LoginRequest request, CancellationToken cancellationToken)
	{
		var command = new LoginCommand(request.Email);

		Result<string> tokenResult = await Sender.Send(command, cancellationToken);

		if (tokenResult.IsFailure)
		{
			return BadRequest(tokenResult.Errors);
		}

		return Ok(tokenResult.Value);
	}
}
