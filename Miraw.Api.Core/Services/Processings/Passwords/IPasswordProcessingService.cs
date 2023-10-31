using Miraw.Api.Core.Models.Passwords;

namespace Miraw.Api.Core.Services.Processings.Passwords;

public interface IPasswordProcessingService
{
	ValueTask<Password> CreatePasswordAsync(Password password);
	void ValidatePasswordString(string passwordString);
	string HashPasswordString(string passwordString);
	void VerifyPasswordString(string passwordString, string hashedPasswordString);
}