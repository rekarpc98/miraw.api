using Xeptions;

namespace Miraw.Api.Core.Models.Users.Exceptions;

public class UserValidationException : Xeption
{
	public UserValidationException(Exception innerException) : base(message: "Invalid input, contact support.",
		innerException)
	{
	}
}