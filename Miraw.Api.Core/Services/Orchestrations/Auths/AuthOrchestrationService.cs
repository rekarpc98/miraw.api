using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Services.Processings.Passwords;
using Miraw.Api.Core.Services.Processings.Users;
using Miraw.Api.Core.Utilities.Securities;

namespace Miraw.Api.Core.Services.Orchestrations.Auths;

public class AuthOrchestrationService : IAuthOrchestrationService
{
	private readonly IPasswordProcessingService passwordProcessingService;
	private readonly IUserProcessingService userProcessingService;
	private readonly ILoggingBroker loggingBroker;

	public AuthOrchestrationService(
		IPasswordProcessingService passwordProcessingService,
		IUserProcessingService userProcessingService,
		ILoggingBroker loggingBroker,
		IJwtTokenGenerator jwtTokenGenerator
	)
	{
		this.passwordProcessingService = passwordProcessingService;
		this.userProcessingService = userProcessingService;
		this.loggingBroker = loggingBroker;
	}

	public async ValueTask<string> CreateAuthTokenForUserAsync(Guid userId)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<string> LoginUserAsync(string phoneNumber, string password)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Guid> ValidateAuthTokenAsync(string authToken)
	{
		throw new NotImplementedException();
	}

	public async ValueTask LogoutUserAsync(Guid userId)
	{
		throw new NotImplementedException();
	}
}