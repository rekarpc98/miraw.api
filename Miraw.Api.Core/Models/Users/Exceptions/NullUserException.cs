using Xeptions;

namespace Miraw.Api.Core.Models.Users.Exceptions;

public class NullUserException : Xeption
{
	public NullUserException() : base(message: "The user is null.")
	{
	}
}