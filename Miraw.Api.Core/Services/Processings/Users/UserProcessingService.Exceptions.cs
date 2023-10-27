using Miraw.Api.Core.Models.Processings.Users;

namespace Miraw.Api.Core.Services.Processings.Users;

public partial class UserProcessingService
{
	private Exception CreateUserProcessingDependencyValidationExceptionAndLogIt(Exception exception)
	{
		var userProcessingDependencyValidationException =
			new UserProcessingDependencyValidationException(exception);

		loggingBroker.LogError(userProcessingDependencyValidationException);

		return userProcessingDependencyValidationException;
	}
}