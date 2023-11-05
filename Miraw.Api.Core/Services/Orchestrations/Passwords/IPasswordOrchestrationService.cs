
using Miraw.Api.Core.Models.Passwords;

namespace Miraw.Api.Core.Services.Orchestrations.Passwords;

public interface IPasswordOrchestrationService
{
	ValueTask<Password> CreatePasswordForUserAsync(Guid userId, string passwordString);
}