using Miraw.Api.Core.Models.Orchestrations.Processings.Regions;
using Miraw.Api.Core.Models.Orchestrations.Users.Exception;
using Miraw.Api.Core.Models.Regions.Exceptions;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Models.Users.Exceptions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Orchestrations.Users;

public partial class UserOrchestrationServiceTests
{
	[Fact]
	public async Task ShouldThrowUserOrchestrationDependencyValidationExceptionWhenRegionIsNotExistsAndLogItAsync()
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

	[Fact]
	public async Task ShouldThrowUserOrchestrationDependencyValidationExceptionWhenUserIsInvalidAndLogItAsync()
	{
		// given
		User invalidUser = CreateInvalidUser(invalidEmail: true, invalidPhoneNumber: false);
		User inputUser = invalidUser;

		var invalidUserException = new InvalidUserException();

		invalidUserException.UpsertDataList(
			key: nameof(User.Email),
			value: "Email is invalid");

		invalidUserException.UpsertDataList(
			key: nameof(User.PhoneNumber),
			value: "PhoneNumber is invalid");

		var userValidationException = new UserValidationException(invalidUserException);

		regionProcessingServiceMock.Setup(x => x.ThrowIfRegionNotExistsAsync(inputUser.RegionId));
		
		userProcessingServiceMock.Setup(x => x.RegisterUserAsync(inputUser))
			.ThrowsAsync(userValidationException);

		var expectedUserOrchestrationException =
			new UserOrchestrationDependencyValidationException(invalidUserException);

		// when
		ValueTask<User> registerUserTask = userOrchestrationService.CreateUserAsync(inputUser);

		// then
		await Assert.ThrowsAsync<UserOrchestrationDependencyValidationException>(() => registerUserTask.AsTask());

		loggingBrokerMock.Verify(x => x.LogError(It.Is(SameExceptionAs(expectedUserOrchestrationException))));
		userProcessingServiceMock.Verify(x => x.RegisterUserAsync(It.IsAny<User>()), Times.Never);

		regionProcessingServiceMock.Verify(x =>
				x.ThrowIfRegionNotExistsAsync(inputUser.RegionId),
			Times.Once);

		regionProcessingServiceMock.VerifyNoOtherCalls();
		userProcessingServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}