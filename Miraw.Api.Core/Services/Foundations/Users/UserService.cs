using Miraw.Api.Core.Brokers.DateTimes;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Services.Foundations.Users;

public partial class UserService : IUserService
{
	readonly ILoggingBroker _loggingBroker;
	readonly IStorageBroker _storageBroker;

	public UserService(IDateTimeBroker dateTimeBroker, ILoggingBroker loggingBroker, IStorageBroker storageBroker)
	{
		_loggingBroker = loggingBroker;
		_storageBroker = storageBroker;
	}

	public ValueTask<User> RegisterUserAsync(User user) => TryCatch(async () =>
	{
		ValidateUserOnRegister(user);

		return await _storageBroker.InsertUserAsync(user);
	});

	public IQueryable<User> RetrieveAllUsers()
	{
		throw new NotImplementedException();
	}

	public async ValueTask<User> RetrieveUserByIdAsync(Guid userId)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<User> ModifyUserAsync(User user)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<User> RemoveUserByIdAsync(Guid userId)
	{
		throw new NotImplementedException();
	}
}