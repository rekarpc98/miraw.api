using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Services.Processings.Regions;
using Miraw.Api.Core.Services.Processings.Users;

namespace Miraw.Api.Core.Services.Orchestrations.Users;

public partial class UserOrchestrationService : IUserOrchestrationService
{
	private readonly IUserProcessingService userProcessingService;
	private readonly IRegionProcessingService regionProcessingService;
	private readonly ILoggingBroker loggingBroker;

	public UserOrchestrationService(IUserProcessingService userProcessingService,
		IRegionProcessingService regionProcessingService, ILoggingBroker loggingBroker)
	{
		this.userProcessingService = userProcessingService;
		this.regionProcessingService = regionProcessingService;
		this.loggingBroker = loggingBroker;
	}

	public async ValueTask<User> CreateUserAsync(User user, string password) =>
		await TryCatch(async () =>
			{
				await regionProcessingService.ThrowIfRegionNotExistsAsync(user.RegionId);
				return await userProcessingService.RegisterUserAsync(user);
			}
		);
}