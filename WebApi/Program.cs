using Application;
using Infrastructure;
using Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation;
using Serilog;
using WebApi.Middlewares;
using WebApi.OptionsSetup;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
	.AddApplication()
	.AddPersistence(builder.Configuration)
	.AddInfrastructure()
	.AddPresentation();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer();

builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddLogging(loggingBuilder =>
{
	loggingBuilder.AddConsole();
});
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
//builder.Host.UseSerilog((context, config) =>
//	config.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

