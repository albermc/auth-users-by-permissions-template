using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>((optionsBuilder) =>
		{
			string connectionString = configuration.GetConnectionString("Database")!;
			optionsBuilder.UseSqlServer(connectionString);
		});

		services.AddTransient<IUserRepository, UserRepository>();
		return services;
	}
}
