using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Services.Processings.Users;

public interface IUserProcessingService
{
	ValueTask VerifyUserExistsAsync(Guid userId);
	ValueTask<User> RegisterUserAsync(User user);
	int RetrieveUsersCount();
	ValueTask<User> RetrieveUserByPhoneNumberAsync(string phoneNumber);
}