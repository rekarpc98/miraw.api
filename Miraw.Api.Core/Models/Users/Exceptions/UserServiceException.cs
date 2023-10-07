using Xeptions;

namespace Miraw.Api.Core.Models.Users.Exceptions;

public class UserServiceException : Xeption
{
	public UserServiceException(Exception innerException)
		: base(message: "User service error occurred, contact support.", innerException)
	{
	}
}