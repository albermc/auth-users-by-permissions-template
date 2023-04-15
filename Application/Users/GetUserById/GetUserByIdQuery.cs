using Application.Abstractions.Messaging;
using Domain.Entities.Users;

namespace Application.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<User>;
