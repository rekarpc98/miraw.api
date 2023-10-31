using Xeptions;

namespace Miraw.Api.Core.Models.Passwords.Exceptions;

public class IncorrectPasswordException : Xeption
{
	public IncorrectPasswordException()
		: base(message: "Incorrect password, please try again.")
	{
	}
}