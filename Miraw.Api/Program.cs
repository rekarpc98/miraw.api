using System.Text;
using Exceptionless;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Miraw.Api.Core.Brokers.DateTimes;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Services.Foundations.Operators;
using Miraw.Api.Core.Services.Foundations.Passwords;
using Miraw.Api.Core.Services.Foundations.Regions;
using Miraw.Api.Core.Services.Foundations.Users;
using Miraw.Api.Core.Services.Foundations.ZoneOperators;
using Miraw.Api.Core.Services.Foundations.Zones;
using Miraw.Api.Core.Services.Orchestrations.Auths;
using Miraw.Api.Core.Services.Orchestrations.Passwords;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddBrokers(builder.Services);
AddFoundationServices(builder.Services);
AddOrchestrationServices(builder.Services);

string? exceptionlessApiKey = builder.Configuration.GetSection("Exceptionless:ApiKey").Value;
builder.Services.AddExceptionless(exceptionlessApiKey!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
		};
	});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionless();

app.UseAuthorization();

app.MapControllers();

app.Run();

return;

static void AddOrchestrationServices(IServiceCollection services)
{
	services.AddScoped<IPasswordOrchestrationService, PasswordOrchestrationService>();
	services.AddScoped<IAuthOrchestrationService, AuthOrchestrationService>();
}

static void AddFoundationServices(IServiceCollection services)
{
	services.AddScoped<IUserService, UserService>()
		.AddScoped<IRegionService, RegionService>()
		.AddScoped<IPasswordService, PasswordService>()
		.AddScoped<IZoneService, ZoneService>()
		.AddScoped<IZoneOperatorService, ZoneOperatorService>()
		.AddScoped<IOperatorService, OperatorService>();
}

static void AddBrokers(IServiceCollection services)
{
	services.AddScoped<IStorageBroker, StorageBroker>();
	services.AddScoped<IDateTimeBroker, DateTimeBroker>();
	services.AddScoped<ILoggingBroker, LoggingBroker>();
}