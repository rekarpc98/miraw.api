using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Users;

public partial class UserServiceTests
{
	[Fact]
	public async Task ShouldModifyUserAsync()
	{
		// given
		var randomDateTime = GetRandomDateTime();

		var randomUser = CreateRandomUser();

		var inputUser = randomUser;

		var afterUpdateStorageUser = inputUser;

		var expectedUser = afterUpdateStorageUser;

		var beforeUpdateStorageUser = randomUser.DeepClone();

		inputUser.UpdatedDate = randomDateTime;

		var userId = inputUser.Id;

		_storageBrokerMock.Setup(x => x.SelectUserByIdAsync(userId)).ReturnsAsync(beforeUpdateStorageUser);

		_storageBrokerMock.Setup(x => x.UpdateUserAsync(inputUser)).ReturnsAsync(afterUpdateStorageUser);

		_dateTimeBrokerMock.Setup(x => x.GetCurrentDateTime()).Returns(randomDateTime);

		// when
		var actualUser = await _userService.ModifyUserAsync(inputUser);

		// then
		actualUser.Should().BeEquivalentTo(expectedUser);

		_storageBrokerMock.Verify(x => x.SelectUserByIdAsync(userId), Times.Once);

		_dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Once);
		
		_storageBrokerMock.Verify(x => x.UpdateUserAsync(inputUser), Times.Once);
		
		_dateTimeBrokerMock.VerifyNoOtherCalls();
		_storageBrokerMock.VerifyNoOtherCalls();
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
}