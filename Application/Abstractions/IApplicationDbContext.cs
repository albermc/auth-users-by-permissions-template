using Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions;

public interface IApplicationDbContext
{
	DbSet<User> Users { get; set; }
	Task<int> SaveChangesAsync(CancellationToken cancellationToken);


}
