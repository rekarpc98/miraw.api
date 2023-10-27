using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Services.Processings.Regions;
using Miraw.Api.Core.Services.Processings.Users;

namespace Miraw.Api.Core.Services.Orchestrations.Users;

public class UserOrchestrationService : IUserOrchestrationService
{
	private readonly IUserProcessingService userProcessingService;
	private readonly IRegionProcessingService regionProcessingService;

	public UserOrchestrationService(IUserProcessingService userProcessingService,
		IRegionProcessingService regionProcessingService)
	{
		this.userProcessingService = userProcessingService;
		this.regionProcessingService = regionProcessingService;
	}

	public async ValueTask<User> CreateUserAsync(User user)
	{
		await regionProcessingService.VerifyRegionExistsAsync(user.RegionId);
		return await userProcessingService.RegisterUserAsync(user);
	}
}