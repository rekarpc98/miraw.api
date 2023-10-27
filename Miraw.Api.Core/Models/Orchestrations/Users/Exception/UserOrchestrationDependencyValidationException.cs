using Xeptions;

namespace Miraw.Api.Core.Models.Orchestrations.Users.Exception;

public class UserOrchestrationDependencyValidationException : Xeption
{
	public UserOrchestrationDependencyValidationException(System.Exception innerException) : base(
		"User service error occured", innerException)
	{
	}
}