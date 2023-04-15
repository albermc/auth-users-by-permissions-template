using Domain.Entities.Users;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
	private readonly ApplicationDbContext _context;
	public UserRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public Task AddAsync(User user, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<bool> AnyWithEmailAsync(Email email, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
	{
		return await _context.Set<User>()
			.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
	}

	public Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}
}
