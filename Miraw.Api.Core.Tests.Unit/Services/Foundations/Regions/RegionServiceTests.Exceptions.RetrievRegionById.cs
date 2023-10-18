using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Regions.Exceptions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Regions;

public partial class RegionServiceTests
{
	[Fact]
	public void ShouldThrowValidationExceptionOnRetrieveWhenRegionNotFoundAndLogItAsync()
	{
		// given
		var randomId = Guid.NewGuid();
		Guid inputRegionId = randomId;

		var notFoundRegionException =
			new NotFoundRegionException(inputRegionId);
        
		var expectedRegionValidationException =
			new RegionValidationException(notFoundRegionException);

		// when
		ValueTask<Region> retrieveRegionTask =
			_regionService.GetRegionAsync(inputRegionId);

		// then
		Assert.ThrowsAsync<RegionValidationException>(() =>
			retrieveRegionTask.AsTask());

		_loggingBrokerMock.Verify(x =>
				x.LogError(It.Is(SameExceptionAs(expectedRegionValidationException))),
			Times.Once());
	}
}