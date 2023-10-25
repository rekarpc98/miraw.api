using EFxceptions.Models.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Models.Users.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Users;

public partial class UserService
{
	private delegate ValueTask<User> ReturningUserFunction();

	private delegate IQueryable<User> ReturningUsersFunction();

	private async ValueTask<User> TryCatch(ReturningUserFunction function)
	{
		try
		{
			return await function();
		}
		catch (NullUserException nullUserException)
		{
			throw CreateAndLogValidationException(nullUserException);
		}
		catch (InvalidUserException invalidUserException)
		{
			throw CreateAndLogValidationException(invalidUserException);
		}
		catch (NotFoundUserException notFoundUserException)
		{
			throw CreateAndLogValidationException(notFoundUserException);
		}
		catch (SqlException ex)
		{
			var failedUserStorageException = new FailedUserStorageException(ex);
			
			throw CreateAndLogCriticalDependencyException(failedUserStorageException);
		}
		catch (DuplicateKeyException duplicateKeyException)
		{
			var alreadyExistsStudentException = new AlreadyExistsUserException(duplicateKeyException);

			throw CreateAndLogDependencyValidationException(alreadyExistsStudentException);
		}
		catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
		{
			var lockedUserException = new LockedUserException(dbUpdateConcurrencyException);

			throw CreateAndLogDependencyException(lockedUserException);
		}
		catch (DbUpdateException dbUpdateException)
		{
			var failedUserStorageException = new FailedUserStorageException(dbUpdateException);

			throw CreateAndLogDependencyException(failedUserStorageException);
		}
		catch (Exception exception)
		{
			var failedUserServiceException = new FailedUserServiceException(exception);

			throw CreateAndLogServiceException(failedUserServiceException);
		}
	}

	private IQueryable<User> TryCatch(ReturningUsersFunction function)
	{
		try
		{
			return function();
		}
		catch (SqlException ex)
		{
			var failedUserStorageException = new FailedUserStorageException(ex);
			
			throw CreateAndLogCriticalDependencyException(failedUserStorageException);
		}
		catch (Exception exception)
		{
			var failedUserServiceException = new FailedUserServiceException(exception);

			throw CreateAndLogServiceException(failedUserServiceException);
		}
	}

	private Exception CreateAndLogDependencyValidationException(AlreadyExistsUserException alreadyExistsStudentException)
	{
		var validationException = new UserValidationException(alreadyExistsStudentException);
		loggingBroker.LogError(validationException);

		return validationException;
	}

	private Exception CreateAndLogDependencyException(Exception exception)
	{
		var userDependencyException = new UserDependencyException(exception);
		loggingBroker.LogError(userDependencyException);

		return userDependencyException;
	}

	private Exception CreateAndLogCriticalDependencyException(Exception exception)
	{
		var validationException = new UserValidationException(exception);
		loggingBroker.LogCritical(validationException);
		
		return validationException;
	}

	private Exception CreateAndLogValidationException(Exception exception)
	{
		var userDependencyValidationException = new UserValidationException(exception);
		loggingBroker.LogError(userDependencyValidationException);
		
		return userDependencyValidationException;
	}

	private Exception CreateAndLogServiceException(Exception exception)
	{
		var userServiceException = new UserServiceException(exception);
		loggingBroker.LogError(userServiceException);
		
		return userServiceException;
	}
}