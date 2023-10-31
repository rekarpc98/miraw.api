using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Models.Passwords.Exceptions;
using Miraw.Api.Core.Services.Foundations.Passwords;

namespace Miraw.Api.Core.Services.Processings.Passwords;

public partial class PasswordProcessingService : IPasswordProcessingService
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
		try
		{
			ValidateString(passwordString);
		}
		catch (InvalidPasswordException invalidPasswordException)
		{
			var passwordProcessingValidationException =
				new PasswordProcessingValidationException(invalidPasswordException);
			
			loggingBroker.LogError(passwordProcessingValidationException);
			
			throw passwordProcessingValidationException; 
		}
	}
}