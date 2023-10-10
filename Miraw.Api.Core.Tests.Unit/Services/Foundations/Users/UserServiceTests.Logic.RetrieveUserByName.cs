using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Users;

public partial class UserServiceTests
{
	[Fact]
	public async Task ShouldRetrieveUserByName()
	{
		// given
		DateTimeOffset randomDateTime = GetRandomDateTime();
		User randomUser = CreateRandomUser(randomDateTime);
		User storageUser = randomUser.DeepClone();
		User expectedUser = storageUser.DeepClone();
		var userName = randomUser.Name;

		_storageBrokerMock.Setup(broker => broker.SelectUserByNameAsync(userName)).ReturnsAsync(storageUser);
		
		// when
		User actualUser = await _userService.RetrieveUserByNameAsync(userName);
		
		// then
		actualUser.Should().BeEquivalentTo(expectedUser);
		
		_storageBrokerMock.Verify(x => x.SelectUserByNameAsync(userName), Times.Once);
		_dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Never);
		
		_dateTimeBrokerMock.VerifyNoOtherCalls();
		_storageBrokerMock.VerifyNoOtherCalls();
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
	
	[Theory]
	[InlineData("Mhammad","Mhammad")]
	[InlineData("Mhammad","Mhommad")]
	[InlineData("Mhammad","Mhamad")]
	[InlineData("Mhammad","Mhammd")]
	[InlineData("Mhammad","Mhamma")]
	[InlineData("Mhammad","Mhamm")]
	[InlineData("Mhammad","Mham")]
	[InlineData("Mhammad","Mha")]
	[InlineData("Mhammad","Mh")]
	[InlineData("Mhammad","M")]
	public async Task ShouldRetrieveUserBySlightlyDifferentName(string userName, string searchWord)
	{
		// given
		DateTimeOffset randomDateTime = GetRandomDateTime();
		User randomUser = CreateRandomUser(randomDateTime, userName);
		User storageUser = randomUser.DeepClone();
		User expectedUser = storageUser.DeepClone();
		
		_storageBrokerMock.Setup(broker => broker.SelectUserByNameAsync(searchWord)).ReturnsAsync(storageUser);
		
		// when
		User actualUser = await _userService.RetrieveUserByNameAsync(searchWord);
		
		// then
		actualUser.Should().BeEquivalentTo(expectedUser);
		
		_storageBrokerMock.Verify(x => x.SelectUserByNameAsync(searchWord), Times.Once);
		_dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Never);
		
		_dateTimeBrokerMock.VerifyNoOtherCalls();
		_storageBrokerMock.VerifyNoOtherCalls();
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
}