using Xeptions;

namespace Miraw.Api.Core.Models.Orchestrations.Users.Exception;

public class UserOrchestrationException : Xeption
{
	public UserOrchestrationException(System.Exception innerException) : base("User service error occured",
		innerException)
	{
	}
}