using Xeptions;

namespace Miraw.Api.Core.Models.Orchestrations.Users.Exception;

public class AlreadyExistsOrchestrationValidationException : Xeption
{
	public AlreadyExistsOrchestrationValidationException(string message) : base(message)
	{
	}
}