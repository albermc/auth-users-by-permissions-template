using Domain.Primitives;

namespace Domain.Repositories;

public interface IRepository<T>
	where T : AggregateRoot
{
}
