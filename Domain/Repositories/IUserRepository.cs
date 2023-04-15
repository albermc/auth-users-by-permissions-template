using Domain.Entities.Users;

namespace Domain.Repositories;

public interface IUserRepository
{
	Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
	Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
	Task<bool> AnyWithEmailAsync(Email email, CancellationToken cancellationToken = default);
	Task AddAsync(User user, CancellationToken cancellationToken = default);

}
