using Application.Abstractions.Messaging;
using Domain.Entities.Users;
using Domain.Repositories;
using Domain.Shared;
using static Domain.Errors.DomainErrors;

namespace Application.Users.GetUserById;

internal sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, User?>
{
	private readonly IUserRepository _userRepository;

	public GetUserByIdQueryHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<Result<User?>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		return await _userRepository.GetByIdAsync(request.Id, cancellationToken);
	}
}
