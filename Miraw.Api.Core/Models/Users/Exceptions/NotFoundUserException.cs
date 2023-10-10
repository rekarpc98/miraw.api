using Xeptions;

namespace Miraw.Api.Core.Models.Users.Exceptions;

public class NotFoundUserException : Xeption
{
	public NotFoundUserException(Guid userId) : base(message: $"Couldn't find the user with id: {userId}.")
	{
	}

	public NotFoundUserException(string userName) : base(message: $"Couldn't find the user with name: {userName}.")
	{
	}
}

public class FailedUserStorageException : Xeption
{
	public FailedUserStorageException(Exception innerException)
		: base(message: "Failed to store the user, contact support.", innerException)
	{
	}
}