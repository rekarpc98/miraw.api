using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Regions.Exceptions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Regions;

public partial class RegionServiceTests
{
	[Fact]
	public void ShouldThrowValidationExceptionOnRetrieveWhenIdIsInvalidAndLogItAsync()
	{
		// given
		var invalidRegionId = Guid.NewGuid();

		var notFoundRegionException =
			new NotFoundRegionException(invalidRegionId);
        
		var expectedRegionValidationException =
			new RegionValidationException(notFoundRegionException);

		// when
		ValueTask<Region> retrieveRegionTask =
			_regionService.GetRegionAsync(invalidRegionId);

		// then
		Assert.ThrowsAsync<RegionValidationException>(() =>
			retrieveRegionTask.AsTask());

		_loggingBrokerMock.Verify(x =>
				x.LogError(It.Is(SameExceptionAs(expectedRegionValidationException))),
			Times.Once());
	}
}