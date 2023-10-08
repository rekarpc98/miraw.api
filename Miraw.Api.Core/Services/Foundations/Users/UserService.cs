using Miraw.Api.Core.Brokers.DateTimes;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Services.Foundations.Users;

public partial class UserService : IUserService
{
	readonly IDateTimeBroker _dateTimeBroker;
	readonly ILoggingBroker _loggingBroker;
	readonly IStorageBroker _storageBroker;

	public UserService(IDateTimeBroker dateTimeBroker, ILoggingBroker loggingBroker, IStorageBroker storageBroker)
	{
		_dateTimeBroker = dateTimeBroker;
		_loggingBroker = loggingBroker;
		_storageBroker = storageBroker;
	}

	public ValueTask<User> RegisterUserAsync(User user) => TryCatch(async () =>
	{
		ValidateUserOnRegister(user);

		return await _storageBroker.InsertUserAsync(user);
	});

	public IQueryable<User> RetrieveAllUsers() => TryCatch(() => _storageBroker.SelectAllUsers());

	public async ValueTask<User> RetrieveUserByIdAsync(Guid userId)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<User> ModifyUserAsync(User user) =>
		await TryCatch(async () =>
		{
			ValidateUserOnModify(user);
			
			var maybeUser = await _storageBroker.SelectUserByIdAsync(user.Id);
			
			ValidateStorageUser(maybeUser, user.Id);
			ValidateAgainstStorageUserOnModify(user, maybeUser!);

			return await _storageBroker.UpdateUserAsync(user);
		});

	public async ValueTask<User> RemoveUserByIdAsync(Guid userId)
	{
		throw new NotImplementedException();
	}
}