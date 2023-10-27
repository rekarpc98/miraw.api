using Miraw.Api.Core.Models.Orchestrations.Users.Exception;
using Miraw.Api.Core.Models.Processings.Regions;
using Miraw.Api.Core.Models.Processings.Users;
using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Services.Orchestrations.Users;

public partial class UserOrchestrationService
{
	private delegate ValueTask<User> ReturningUserFunction();

	private async ValueTask<User> TryCatch(ReturningUserFunction returningUserFunction)
	{
		try
		{
			return await returningUserFunction();
		}
		catch (RegionProcessingDependencyValidationException regionProcessingDependencyValidationException)
		{
			Exception innerException = regionProcessingDependencyValidationException.InnerException!;

			throw CreateUserOrchestrationDependencyValidationExceptionAndLogIt(innerException);
		}
		catch (UserProcessingDependencyValidationException userProcessingDependencyValidationException)
		{
			Exception innerException = userProcessingDependencyValidationException.InnerException!;

			throw CreateUserOrchestrationDependencyValidationExceptionAndLogIt(innerException);
		}
	}

	private Exception CreateUserOrchestrationDependencyValidationExceptionAndLogIt(Exception innerException)
	{
		var userOrchestrationDependencyValidationException =
			new UserOrchestrationDependencyValidationException(innerException);

		loggingBroker.LogError(userOrchestrationDependencyValidationException);

		return userOrchestrationDependencyValidationException;
	}
}