using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Models.Users.Exceptions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Users;

public partial class UserServiceTests
{
	[Fact]
	public async Task ShouldThrowUserValidationExceptionOnRegisterIfCreateAndUpdateDatesAreNotSameAndLogItAsync()
	{
		// given
		User randomUser = CreateRandomUser();
		User invalidUser = randomUser;

		invalidUser.UpdatedDate = invalidUser.UpdatedDate.AddDays(3);

		var invalidUserException = new InvalidUserException();

		invalidUserException.AddData(
			key: nameof(User.UpdatedDate),
			values: $"Date is not the same as {nameof(User.CreatedDate)}"
		);

		var expectedUserValidationException = new UserValidationException(invalidUserException);

		// when
		ValueTask<User> registerUserTask = userService.RegisterUserAsync(invalidUser);

		// then
		await Assert.ThrowsAsync<UserValidationException>(() => registerUserTask.AsTask());

		loggingBrokerMock.Verify(x =>
			x.LogError(It.Is(SameExceptionAs(expectedUserValidationException))), Times.Once);

		storageBrokerMock.Verify(x => x.InsertUserAsync(It.IsAny<User>()), Times.Never);
	}
}