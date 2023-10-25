using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Services.Foundations.Users;

namespace Miraw.Api.Core.Services.Processings.Users;

public interface IUserProcessingService
{
	int RetrieveUsersCount();
}

public class UserProcessingService : IUserProcessingService
{
	private readonly IUserService userService;

	public UserProcessingService(IUserService userService)
	{
		this.userService = userService;
	}
	public int RetrieveUsersCount()
	{
		IQueryable<User> users = userService.RetrieveAllUsers();
		
		return users.Count();
	}
}