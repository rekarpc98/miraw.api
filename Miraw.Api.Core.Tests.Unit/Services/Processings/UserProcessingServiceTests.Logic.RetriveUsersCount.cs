using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings;

public partial class UserProcessingServiceTests
{
	[Fact]
	public async Task ShouldRetrieveUsersCountAsync()
	{
		// given
		IQueryable<User> randomUsers = CreateRandomUsers();
		IQueryable<User> storageUsers = randomUsers;
		int expectedUsersCount = storageUsers.Count();

		userServiceMock.Setup(x => x.RetrieveAllUsers()).Returns(storageUsers);
		
		// when
		int actualUsersCount = await userProcessingService.RetrieveUsersCountAsync();

		// then
		
		Assert.Equal(expectedUsersCount, actualUsersCount);
		
		userServiceMock.Verify(x => x.RetrieveAllUsers(), Times.Once);
		
		userServiceMock.VerifyNoOtherCalls();
	}
}