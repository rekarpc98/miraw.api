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

		storageBrokerMock.Setup(broker => broker.SelectUsersByName(userName)).Returns(storageUsers);
		
		// when
		IQueryable<User> actualUser = userService.RetrieveUsersByName(userName);
		
		// then
		actualUser.Should().BeEquivalentTo(expectedUsers);
		
		storageBrokerMock.Verify(x => x.SelectUsersByName(userName), Times.Once);
		dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Never);
		
		dateTimeBrokerMock.VerifyNoOtherCalls();
		storageBrokerMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
	
	[Fact]
	public async Task ShouldThrowUserServiceExceptionOnEmptyOrNullSearchKeywordAndLogIt()
	{
		// given
		DateTimeOffset randomDateTime = GetRandomDateTime();
		var randomUsers = CreateRandomUsers(randomDateTime);
		var storageUser = randomUsers.DeepClone();
		
		storageBrokerMock.Setup(broker => broker.SelectUsersByName("")).Returns(storageUser);
		
		// when
		// ---
		
		// then
		
		await Assert.ThrowsAsync<UserServiceException>( async () =>
		{
			userService.RetrieveUsersByName("");
			Task.CompletedTask.Wait();
		});
		
		storageBrokerMock.Verify(x => x.SelectUsersByName(""), Times.Never);
		dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Never);
		loggingBrokerMock.Verify(x => x.LogError(It.IsAny<UserServiceException>()),Times.Once);
		
		dateTimeBrokerMock.VerifyNoOtherCalls();
		storageBrokerMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}