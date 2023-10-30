using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Services.Foundations.Users;

namespace Miraw.Api.Core.Services.Processings.Users;

public partial class UserProcessingService : IUserProcessingService
{
	private readonly IUserService userService;
	private readonly ILoggingBroker loggingBroker;

	public UserProcessingService(IUserService userService, ILoggingBroker loggingBroker)
	{
		this.userService = userService;
		this.loggingBroker = loggingBroker;
	}

	public async ValueTask<User> RegisterUserAsync(User user) => await userService.RegisterUserAsync(user);

	public int RetrieveUsersCount()
	{
		try
		{
			IQueryable<User> users = userService.RetrieveAllUsers();
			return users.Count();
		}
		catch (Exception exception)
		{
			throw CreateUserProcessingDependencyValidationExceptionAndLogIt(exception);
		}
	}

	public async ValueTask<User> RetrieveUserByPhoneNumberAsync(string phoneNumber)
	{
		return await userService.RetrieveUserByPhoneNumberAsync(phoneNumber);
	}
}