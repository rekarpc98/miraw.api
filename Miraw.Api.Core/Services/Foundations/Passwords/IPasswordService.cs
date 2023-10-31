using Miraw.Api.Core.Models.Passwords;

namespace Miraw.Api.Core.Services.Foundations.Passwords;

public interface IPasswordService
{
	ValueTask<Password> CreatePasswordAsync(Password password);
	ValueTask<Password> UpdatePasswordAsync(Password password);
	ValueTask<Password> RetrievePasswordByUserIdAsync(Guid userId);
}