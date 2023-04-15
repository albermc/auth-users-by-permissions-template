﻿using Application.Abstractions;
using Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		services.AddTransient<IJwtProvider, JwtProvider>();
		return services;
	}
}
