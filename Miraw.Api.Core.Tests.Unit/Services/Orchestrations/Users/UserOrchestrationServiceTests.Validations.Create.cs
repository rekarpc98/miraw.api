using Miraw.Api.Core.Models.Orchestrations.Processings.Regions;
using Miraw.Api.Core.Models.Orchestrations.Users.Exception;
using Miraw.Api.Core.Models.Regions.Exceptions;
using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Orchestrations.Users;

public partial class UserOrchestrationServiceTests
{
	[Fact]
	public async Task ShouldThrowUserOrchestrationExceptionWhenRegionIsNotExistsAndLogItAsync()
	{
		// given
		Guid invalidRegionId = Guid.NewGuid();
		Guid userRegionId = invalidRegionId;
		User randomUser = CreateRandomUser(regionId: userRegionId);

		var notfoundRegionException = new NotFoundRegionException(invalidRegionId);

		var regionProcessingDependencyValidationException =
			new RegionProcessingDependencyValidationException(notfoundRegionException);

		regionProcessingServiceMock.Setup(x =>
				x.ThrowIfRegionNotExistsAsync(userRegionId))
			.ThrowsAsync(regionProcessingDependencyValidationException);


		var expectedUserOrchestrationException =
			new UserOrchestrationDependencyValidationException(notfoundRegionException);

		// when
		ValueTask<User> registerUserTask = userOrchestrationService.CreateUserAsync(randomUser);

		// then
		await Assert.ThrowsAsync<UserOrchestrationDependencyValidationException>(() => registerUserTask.AsTask());
		
		loggingBrokerMock.Verify(x => x.LogError(It.Is(SameExceptionAs(expectedUserOrchestrationException))));
		userProcessingServiceMock.Verify(x => x.RegisterUserAsync(It.IsAny<User>()), Times.Never);
		regionProcessingServiceMock.Verify(x => x.ThrowIfRegionNotExistsAsync(userRegionId), Times.Once);
		
		regionProcessingServiceMock.VerifyNoOtherCalls();
		userProcessingServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}