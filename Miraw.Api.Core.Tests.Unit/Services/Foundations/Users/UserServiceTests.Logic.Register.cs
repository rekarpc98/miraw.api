using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Users;

public partial class UserServiceTests
{
	[Fact]
	public async Task ShouldRegisterUserAsync()
	{
		// given
		DateTimeOffset randomDateTime = GetRandomDateTime();
		var dateTime = randomDateTime;
		User randomUser = CreateRandomUser(randomDateTime);

		User inputUser = randomUser.DeepClone();
		User storageUser = inputUser.DeepClone();
		User expectedUser = storageUser.DeepClone();

		dateTimeBrokerMock.Setup(broker => broker.GetCurrentDateTime()).Returns(dateTime);
		
		storageBrokerMock.Setup(broker => broker.InsertUserAsync(inputUser)).ReturnsAsync(storageUser);
		
		// when
		User actualUser = await userService.RegisterUserAsync(inputUser);
		
		// then
		actualUser.Should().BeEquivalentTo(expectedUser);
		
		storageBrokerMock.Verify(x => x.InsertUserAsync(inputUser), Times.Once);
		
		dateTimeBrokerMock.VerifyNoOtherCalls();
		storageBrokerMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}