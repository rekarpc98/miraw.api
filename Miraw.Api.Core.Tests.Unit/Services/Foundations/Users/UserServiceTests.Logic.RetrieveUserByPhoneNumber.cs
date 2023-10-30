using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Users;

public partial class UserServiceTests
{
	[Fact]
	public async Task ShouldRetrieveUserByPhoneNumberAsync()
	{
		// given
		User randomUser = CreateRandomUser();
		User inputUser = randomUser;
		string inputPhoneNumber = inputUser.PhoneNumber;
		User storageUser = inputUser;
		User expectedUser = storageUser.DeepClone();

		storageBrokerMock.Setup(x =>
				x.SelectUsersByPhoneNumber(inputPhoneNumber))
			.ReturnsAsync(storageUser);

		// when
		User actualUser = await userService.RetrieveUserByPhoneNumberAsync(inputPhoneNumber);

		// then
		actualUser.Should().BeEquivalentTo(expectedUser);
		storageBrokerMock.Verify(x => x.SelectUsersByPhoneNumber(inputPhoneNumber), Times.Once);
		storageBrokerMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}