using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Services.Foundations.Passwords;

namespace Miraw.Api.Core.Services.Processings.Passwords;

public class PasswordProcessingService : IPasswordProcessingService
{
	private readonly IPasswordService passwordService;

	public PasswordProcessingService(IPasswordService passwordService)
	{
		this.passwordService = passwordService;
	}
	
	public async ValueTask<Password> CreatePasswordAsync(Password password)
	{
		throw new NotImplementedException();
	}
}