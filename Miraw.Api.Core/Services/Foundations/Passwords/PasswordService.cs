using Miraw.Api.Core.Models.Passwords;

namespace Miraw.Api.Core.Services.Foundations.Passwords;

public class PasswordService : IPasswordService
{
	public async ValueTask<Password> CreatePasswordAsync(Password password)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Password> UpdatePasswordAsync(Password password)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Password> RetrievePasswordByUserIdAsync(Guid userId)
	{
		throw new NotImplementedException();
	}
}