using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.Users.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, string>
{
	private readonly IJwtProvider _jwtProvider;
	private readonly IUserRepository _userRepository;

	public LoginCommandHandler(IJwtProvider jwtProvider, IUserRepository userRepository)
	{
		_jwtProvider = jwtProvider;
		_userRepository = userRepository;
	}

	public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		Result<Email> email = Email.Create(request.Email);

		if (email.IsFailure)
		{
			return Result.Failure<string>(email.Errors);
		}
		User? user = await _userRepository.GetByEmailAsync(email.Value, cancellationToken);

		if (user is null)
		{
			return Result.Failure<string>(DomainErrors.Users.InvalidCredentials);
		}

		string token = _jwtProvider.Generate(user);

		return token;
	}
}
