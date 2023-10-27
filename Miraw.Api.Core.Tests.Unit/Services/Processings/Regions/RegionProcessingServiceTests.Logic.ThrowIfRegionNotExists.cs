using Miraw.Api.Core.Models.Orchestrations.Processings.Regions;
using Miraw.Api.Core.Models.Regions.Exceptions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Regions;

public partial class RegionProcessingServiceTests
{
	[Fact]
	public async Task ShouldThrowRegionProcessingDependencyValidationExceptionIfRegionNotFoundAndLogItAsync()
	{
		// given
		Guid invalidRegionId = Guid.NewGuid();


		var notFoundRegionException = new NotFoundRegionException(invalidRegionId);

		var regionValidationException = new RegionValidationException(notFoundRegionException);

		var expectedRegionProcessingValidationException =
			new RegionProcessingDependencyValidationException(notFoundRegionException);

		regionServiceMock.Setup(x =>
				x.RetrieveRegionAsync(invalidRegionId))!
			.ThrowsAsync(regionValidationException);
		// when
		ValueTask throwIfRegionNotExistsTask = regionProcessingService.ThrowIfRegionNotExistsAsync(invalidRegionId);

		// then
		await Assert.ThrowsAsync<RegionProcessingDependencyValidationException>(() =>
			throwIfRegionNotExistsTask.AsTask());

		loggingBrokerMock.Verify(x =>
			x.LogError(It.Is(SameExceptionAs(expectedRegionProcessingValidationException))));
		
		regionServiceMock.Verify(x =>
			x.RetrieveRegionAsync(invalidRegionId),
			Times.Once);
		
		regionServiceMock.VerifyNoOtherCalls();
		regionServiceMock.VerifyNoOtherCalls();
	}
}