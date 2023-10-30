using System.Runtime.CompilerServices;
using Miraw.Api.Core.Brokers.DateTimes;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Services.Foundations.Users;

public partial class UserService : IUserService
{
	private readonly IDateTimeBroker dateTimeBroker;
	private readonly ILoggingBroker loggingBroker;
	private readonly IStorageBroker storageBroker;

	public UserService(IDateTimeBroker dateTimeBroker, ILoggingBroker loggingBroker, IStorageBroker storageBroker)
	{
		this.dateTimeBroker = dateTimeBroker;
		this.loggingBroker = loggingBroker;
		this.storageBroker = storageBroker;
	}

	public ValueTask<User> RegisterUserAsync(User user) => TryCatch(async () =>
	{
		ValidateUserOnRegister(user);

		return await storageBroker.InsertUserAsync(user);
	});

	public IQueryable<User> RetrieveAllUsers() => TryCatch(() => storageBroker.SelectAllUsers());

	public async ValueTask<User> RetrieveUserByIdAsync(Guid userId)
	{
		throw new NotImplementedException();
	}
	public IQueryable<User> RetrieveUsersByName(string userName)
	{
		return TryCatch(() =>
		{
			ValidateUserName(userName);

			IQueryable<User> users =  storageBroker.SelectUsersByName(userName);

			return users;
		});
	}

	public async ValueTask<User> RetrieveUsersByPhoneNumberAsync(string phoneNumber)
	{
		return await storageBroker.SelectUsersByPhoneNumber(phoneNumber);
	}

	public async ValueTask<User> ModifyUserAsync(User user) =>
		await TryCatch(async () =>
		{
			ValidateUserOnModify(user);
			
			var maybeUser = await storageBroker.SelectUserByIdAsync(user.Id);
			
			ValidateStorageUser(maybeUser, user.Id);
			ValidateAgainstStorageUserOnModify(user, maybeUser!);

			return await storageBroker.UpdateUserAsync(user);
		});

	public async ValueTask<User> RemoveUserByIdAsync(Guid userId)
	{
		throw new NotImplementedException();
	}
}