using Xeptions;

namespace Miraw.Api.Core.Models.Users.Exceptions;

public class UserDependencyException : Xeption
{
	public UserDependencyException(Exception innerException)
		: base(message: "Service dependency error occurred, contact support.", innerException)
	{
	}
}
public class UserDependencyValidationException : Xeption
{
	public UserDependencyValidationException(Exception innerException)
		: base(message: "Service dependency validation error occurred, contact support.", innerException)
	{
	}
}