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

		_dateTimeBrokerMock.Setup(broker => broker.GetCurrentDateTime()).Returns(dateTime);
		
		_storageBrokerMock.Setup(broker => broker.InsertUserAsync(inputUser)).ReturnsAsync(storageUser);
		
		// when
		User actualUser = await _userService.RegisterUserAsync(inputUser);
		
		// then
		actualUser.Should().BeEquivalentTo(expectedUser);
		
		_storageBrokerMock.Verify(x => x.InsertUserAsync(inputUser), Times.Once);
		
		_dateTimeBrokerMock.VerifyNoOtherCalls();
		_storageBrokerMock.VerifyNoOtherCalls();
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
}