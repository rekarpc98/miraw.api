using Xeptions;

namespace Miraw.Api.Core.Models.Users.Exceptions;

public class FailedUserServiceException : Xeption
{
	public FailedUserServiceException(Exception innerException)
		: base(message: "Failed user service error occurred, contact support.", innerException)
	{
	}
}