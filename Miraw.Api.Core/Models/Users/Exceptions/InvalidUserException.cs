using Xeptions;

namespace Miraw.Api.Core.Models.Users.Exceptions;

public class InvalidUserException : Xeption
{
	public InvalidUserException() : base(message: "Invalid user. Please fix the errors and try again.")
	{
	}
}