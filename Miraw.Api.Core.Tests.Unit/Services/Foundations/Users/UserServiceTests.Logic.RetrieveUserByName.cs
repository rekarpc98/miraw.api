using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Models.Users.Exceptions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Users;

public partial class UserServiceTests
{
	[Fact]
	public async Task ShouldRetrieveUserByName()
	{
		// given
		DateTimeOffset randomDateTime = GetRandomDateTime();
		IQueryable<User> randomUsers = CreateRandomUsers(randomDateTime);
		var firstUser = randomUsers.First();
		IQueryable<User> expectedUsers = randomUsers.Where(x => x.Id == firstUser.Id).DeepClone();
		IQueryable<User> storageUsers = randomUsers.Where(x => x.Id == firstUser.Id).DeepClone();
		var userName = firstUser.Name;

		_storageBrokerMock.Setup(broker => broker.SelectUsersByName(userName)).Returns(storageUsers);
		
		// when
		IQueryable<User> actualUser = _userService.RetrieveUsersByName(userName);
		
		// then
		actualUser.Should().BeEquivalentTo(expectedUsers);
		
		_storageBrokerMock.Verify(x => x.SelectUsersByName(userName), Times.Once);
		_dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Never);
		
		_dateTimeBrokerMock.VerifyNoOtherCalls();
		_storageBrokerMock.VerifyNoOtherCalls();
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
	
	[Fact]
	public async Task ShouldThrowUserServiceExceptionOnEmptyOrNullSearchKeywordAndLogIt()
	{
		// given
		DateTimeOffset randomDateTime = GetRandomDateTime();
		var randomUsers = CreateRandomUsers(randomDateTime);
		var storageUser = randomUsers.DeepClone();
		
		_storageBrokerMock.Setup(broker => broker.SelectUsersByName("")).Returns(storageUser);
		
		// when
		// ---
		
		// then
		
		await Assert.ThrowsAsync<UserServiceException>( async () =>
		{
			_userService.RetrieveUsersByName("");
			Task.CompletedTask.Wait();
		});
		
		_storageBrokerMock.Verify(x => x.SelectUsersByName(""), Times.Never);
		_dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Never);
		_loggingBrokerMock.Verify(x => x.LogError(It.IsAny<UserServiceException>()),Times.Once);
		
		_dateTimeBrokerMock.VerifyNoOtherCalls();
		_storageBrokerMock.VerifyNoOtherCalls();
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
}