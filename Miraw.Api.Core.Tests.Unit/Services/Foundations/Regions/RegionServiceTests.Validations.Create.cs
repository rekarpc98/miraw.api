using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Regions.Exceptions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Regions;

public partial class RegionServiceTests
{
	[Fact]
	public async Task ShouldThrowRegionValidationExceptionOnCreateRegionAndLogItAsync()
	{
		// given
		Region? nullRegion = null;
		Region? inputRegion = nullRegion;
		
		var nullRegionException = new NullRegionException();

		var regionValidationException =
			new RegionValidationException(nullRegionException);

		// when
		ValueTask<Region> createRegionTask =
			_regionService.CreateRegionAsync(inputRegion!);

		// then
		await Assert.ThrowsAsync<RegionValidationException>(() =>
			createRegionTask.AsTask());

		_loggingBrokerMock.Verify(broker =>
				broker.LogError(It.IsAny<RegionValidationException>()),
			Times.Once);

		_loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(regionValidationException))),
			Times.Once);


		_storageBrokerMock.Verify(broker =>
				broker.InsertRegionAsync(It.IsAny<Region>()),
			Times.Never);

		_storageBrokerMock.VerifyNoOtherCalls();
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
}