using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Services.Processings.Passwords;
using Miraw.Api.Core.Services.Processings.Users;

namespace Miraw.Api.Core.Services.Orchestrations.Passwords;

public class PasswordOrchestrationService : IPasswordOrchestrationService
{
	private readonly IUserProcessingService userProcessingService;
	private readonly IPasswordProcessingService passwordProcessingService;
	private readonly ILoggingBroker loggingBroker;

	public PasswordOrchestrationService(IUserProcessingService userProcessingService,
		IPasswordProcessingService passwordProcessingService, ILoggingBroker loggingBroker)
	{
		this.userProcessingService = userProcessingService;
		this.passwordProcessingService = passwordProcessingService;
		this.loggingBroker = loggingBroker;
	}

	public async ValueTask<Password> CreatePasswordForUserAsync(Guid userId, string passwordString)
	{
		throw new NotImplementedException();
	}
}