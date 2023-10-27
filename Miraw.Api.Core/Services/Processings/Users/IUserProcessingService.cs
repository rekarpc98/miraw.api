using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Services.Processings.Users;

public interface IUserProcessingService
{
	int RetrieveUsersCount();
	ValueTask<User> RegisterUserAsync(User user);
}