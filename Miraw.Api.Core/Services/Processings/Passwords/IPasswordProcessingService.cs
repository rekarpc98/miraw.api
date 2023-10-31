using Miraw.Api.Core.Models.Passwords;

namespace Miraw.Api.Core.Services.Processings.Passwords;

public interface IPasswordProcessingService
{
	ValueTask<Password> CreatePasswordAsync(Password password);
}