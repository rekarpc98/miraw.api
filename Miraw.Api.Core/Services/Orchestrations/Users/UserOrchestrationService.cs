using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Orchestrations.Processings.Regions;
using Miraw.Api.Core.Models.Orchestrations.Users.Exception;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Services.Processings.Regions;
using Miraw.Api.Core.Services.Processings.Users;

namespace Miraw.Api.Core.Services.Orchestrations.Users;

public class UserOrchestrationService : IUserOrchestrationService
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

	public async ValueTask<User> CreateUserAsync(User user)
	{
		try
		{
			await regionProcessingService.ThrowIfRegionNotExistsAsync(user.RegionId);
			return await userProcessingService.RegisterUserAsync(user);
		}
		catch (RegionProcessingDependencyValidationException regionProcessingDependencyValidationException)
		{
			Exception innerException = regionProcessingDependencyValidationException.InnerException!;
			
			var userOrchestrationDependencyValidationException =
				new UserOrchestrationDependencyValidationException(innerException);
			
			loggingBroker.LogError(userOrchestrationDependencyValidationException);
			
			throw userOrchestrationDependencyValidationException;
		}
	}
}