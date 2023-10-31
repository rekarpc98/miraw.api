using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Services.Orchestrations.Users;

public interface IUserOrchestrationService
{
	ValueTask<User> CreateUserAsync(User user);
}