using Application.Abstractions;
using Domain.Entities.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

internal sealed class JwtProvider : IJwtProvider
{
	private readonly JwtOptions _jwtOptions;
	public JwtProvider(IOptions<JwtOptions> jwtOptions)
	{
		_jwtOptions = jwtOptions.Value;
	}

	public string Generate(User user)
	{
		var claims = new Claim[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Email, user.Email.Value)
		};

		var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
			SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			_jwtOptions.Issuer,
			_jwtOptions.Audience,
			claims,
			null,
			DateTime.UtcNow.AddDays(1),
			signingCredentials);

		string tokenValue = new JwtSecurityTokenHandler()
			.WriteToken(token);

		return tokenValue;
	}
}
