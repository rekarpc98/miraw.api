using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Services.Foundations.Passwords;

namespace Miraw.Api.Core.Services.Processings.Passwords;

public class PasswordProcessingService : IPasswordProcessingService
{
	private readonly IPasswordService passwordService;
	private readonly ILoggingBroker loggingBroker;

	public PasswordProcessingService(IPasswordService passwordService, ILoggingBroker loggingBroker)
	{
		this.passwordService = passwordService;
		this.loggingBroker = loggingBroker;
	}
	
	public async ValueTask<Password> CreatePasswordAsync(Password password) => 
		await passwordService.CreatePasswordAsync(password);

	public void ValidatePasswordString(string passwordString)
	{
		throw new NotImplementedException();
	}
}