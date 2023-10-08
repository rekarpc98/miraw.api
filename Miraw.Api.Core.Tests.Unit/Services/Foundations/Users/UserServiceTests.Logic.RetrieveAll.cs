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

		_storageBrokerMock.Setup(broker => broker.SelectAllUsers()).Returns(storageUsers);
		
		// when
		IQueryable<User> actualUsers = _userService.RetrieveAllUsers();
		
		// then
		actualUsers.Should().BeEquivalentTo(expectedUsers);
		
		_storageBrokerMock.Verify(x => x.SelectAllUsers(), Times.Once);
		_dateTimeBrokerMock.Verify(x => x.GetCurrentDateTime(), Times.Never);
		
		_dateTimeBrokerMock.VerifyNoOtherCalls();
		_storageBrokerMock.VerifyNoOtherCalls();
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
}