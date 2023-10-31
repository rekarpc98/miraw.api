using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Orchestrations.Users.Exception;
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

	public async ValueTask<User> CreateUserAsync(User user) =>
		await TryCatch(async () =>
			{
				await regionProcessingService.ThrowIfRegionNotExistsAsync(user.RegionId);
				
				User existingUser = await userProcessingService.RetrieveUserByPhoneNumberAsync(user.PhoneNumber);
				
				ValidateStorageUser(existingUser);
				
				return await userProcessingService.RegisterUserAsync(user);
			}
		);

	private static void ValidateStorageUser(User? existingUser)
	{
		if (existingUser is not null)
		{
			throw new AlreadyExistsOrchestrationValidationException(
				$"User with phone number {existingUser.PhoneNumber} already exists.");
		}
	}
}