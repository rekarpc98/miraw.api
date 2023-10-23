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

		storageBrokerMock.Setup(x => x.SelectUserByIdAsync(userId)).ReturnsAsync(beforeUpdateStorageUser);

		storageBrokerMock.Setup(x => x.UpdateUserAsync(inputUser)).ReturnsAsync(afterUpdateStorageUser);

		dateTimeBrokerMock.Setup(x => x.GetCurrentDateTime()).Returns(randomDateTime);

		// when
		var actualUser = await userService.ModifyUserAsync(inputUser);

		// then
		actualUser.Should().BeEquivalentTo(expectedUser);

		storageBrokerMock.Verify(x => x.SelectUserByIdAsync(userId), Times.Once);

		dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Once);
		
		storageBrokerMock.Verify(x => x.UpdateUserAsync(inputUser), Times.Once);
		
		dateTimeBrokerMock.VerifyNoOtherCalls();
		storageBrokerMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}