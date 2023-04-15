using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
	private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

	public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
	{
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);
		}
		catch (Exception e)
		{
			_logger.LogError(e, e.Message);

			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ContentType = "application/json";

			ProblemDetails problemDetails = new ProblemDetails()
			{
				Status = (int)HttpStatusCode.InternalServerError,
				Type = "Server error",
				Title = "Server error",
				Detail = $"An internal server has occcurred: {e.Message}"
			};

			await context.Response.WriteAsJsonAsync(problemDetails);
		}
	}
}
