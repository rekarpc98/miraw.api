using Xeptions;

namespace Miraw.Api.Core.Models.Users.Exceptions;

public class LockedUserException : Xeption
{
	public LockedUserException(Exception innerException)
		: base(message: "The user is locked, please try again later or contact support.", innerException)
	{
	}
}