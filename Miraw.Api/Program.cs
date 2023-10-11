using Exceptionless;
using Miraw.Api.Core.Brokers.DateTimes;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Services.Foundations.Users;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IStorageBroker, StorageBroker>();
builder.Services.AddScoped<IDateTimeBroker, DateTimeBroker>();
builder.Services.AddScoped<ILoggingBroker, LoggingBroker>();

// Exceptionless__ApiKey
var exceptionlessApiKey = builder.Configuration.GetSection("Exceptionless:ApiKey").Value;
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