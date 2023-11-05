using Exceptionless;
using Miraw.Api.Core.Brokers.DateTimes;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Services.Foundations.Operators;
using Miraw.Api.Core.Services.Foundations.Passwords;
using Miraw.Api.Core.Services.Foundations.Regions;
using Miraw.Api.Core.Services.Foundations.Users;
using Miraw.Api.Core.Services.Foundations.ZoneOperators;
using Miraw.Api.Core.Services.Foundations.Zones;
using Miraw.Api.Core.Services.Orchestrations.Passwords;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddBrokers(builder.Services, builder.Configuration);
AddFoundationServices(builder.Services, builder.Configuration);
AddOrchestrationServices(builder.Services);

string? exceptionlessApiKey = builder.Configuration.GetSection("Exceptionless:ApiKey").Value;
builder.Services.AddExceptionless(exceptionlessApiKey!);

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
}

static void AddFoundationServices(IServiceCollection services, IConfiguration configuration)
{
	services.AddScoped<IUserService, UserService>()
		.AddScoped<IRegionService, RegionService>()
		.AddScoped<IPasswordService, PasswordService>()
		.AddScoped<IZoneService, ZoneService>()
		.AddScoped<IZoneOperatorService, ZoneOperatorService>()
		.AddScoped<IOperatorService, OperatorService>();
}

static void AddBrokers(IServiceCollection services, IConfiguration configuration)
{
	services.AddScoped<IStorageBroker, StorageBroker>();
	services.AddScoped<IDateTimeBroker, DateTimeBroker>();
	services.AddScoped<ILoggingBroker, LoggingBroker>();
}