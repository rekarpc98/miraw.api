using Xeptions;

namespace Miraw.Api.Core.Models.Users.Exceptions;

public class AlreadyExistsUserException : Xeption
{
	public AlreadyExistsUserException(Exception innerException) : base(message: "User already exists.", innerException)
	{
	}
}