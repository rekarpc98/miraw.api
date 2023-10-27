using Xeptions;

namespace Miraw.Api.Core.Models.Processings.Users;

public class UserProcessingDependencyValidationException : Xeption
{
	public UserProcessingDependencyValidationException(Exception innerException) : base("User service error occured.",
		innerException)
	{
	}
}