using Miraw.Api.Core.Services.Foundations.Users;

namespace Miraw.Api.Core.Services.Processings.Users;

public interface IUserProcessingService
{
	ValueTask<int> RetrieveUsersCountAsync();
}

public class UserProcessingService : IUserProcessingService
{
	private readonly IUserService userService;

	public UserProcessingService(IUserService userService)
	{
		this.userService = userService;
	}
	public async ValueTask<int> RetrieveUsersCountAsync()
	{
		throw new NotImplementedException();
	}
}