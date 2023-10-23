using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Users;

public partial class UserServiceTests
{
	[Fact]
	public async Task ShouldRetrieveAllUsers()
	{
		// given
		DateTimeOffset randomDateTime = GetRandomDateTime();
		IQueryable<User> randomUsers = CreateRandomUsers(randomDateTime);

		IQueryable<User> storageUsers = randomUsers.DeepClone();
		IQueryable<User> expectedUsers = storageUsers.DeepClone();

		storageBrokerMock.Setup(broker => broker.SelectAllUsers()).Returns(storageUsers);
		
		// when
		IQueryable<User> actualUsers = userService.RetrieveAllUsers();
		
		// then
		actualUsers.Should().BeEquivalentTo(expectedUsers);
		
		storageBrokerMock.Verify(x => x.SelectAllUsers(), Times.Once);
		dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Never);
		
		dateTimeBrokerMock.VerifyNoOtherCalls();
		storageBrokerMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}