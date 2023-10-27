using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Orchestrations.Users;

public partial class UserOrchestrationServiceTests
{
	[Fact]
	public async Task ShouldCreateUserAsync()
	{
		// given
		Region randomRegion = CreateRandomRegion();
		Region storageRegion = randomRegion;
		Guid userRegionId = storageRegion.Id;
		User randomUser = CreateRandomUser(regionId: userRegionId);
		User inputUser = randomUser;
		User storageUser = inputUser;
		User expectedUser = inputUser.DeepClone();
		
		regionProcessingServiceMock.Setup(x =>
				x.VerifyRegionExistsAsync(userRegionId))
			.ReturnsAsync(true);
		
		userProcessingServiceMock.Setup(x =>
				x.RegisterUserAsync(inputUser))
			.ReturnsAsync(storageUser);
		
		// when
		User actualUser = await userOrchestrationService.CreateUserAsync(inputUser);
		
		// then
		actualUser.Should().BeEquivalentTo(expectedUser);
		
		regionProcessingServiceMock.Verify(x =>
				x.ThrowIfRegionNotExistsAsync(userRegionId),
			Times.Once);
		
		userProcessingServiceMock.Verify(x =>
				x.RegisterUserAsync(inputUser),
			Times.Once);
		
		regionProcessingServiceMock.VerifyNoOtherCalls();
		userProcessingServiceMock.VerifyNoOtherCalls();
	}
}