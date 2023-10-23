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
		var invalidRegionId = Guid.Empty;
		Guid inputRegionId = invalidRegionId;

		var invalidRegionException =
			new InvalidRegionException(parameterName: nameof(Region.Id), parameterValue: inputRegionId);
        
		var expectedRegionValidationException =
			new RegionValidationException(invalidRegionException);

		// when
		ValueTask<Region> retrieveRegionTask =
			_regionService.RetrieveRegionAsync(inputRegionId);

		// then
		Assert.ThrowsAsync<RegionValidationException>(() =>
			retrieveRegionTask.AsTask());

		_loggingBrokerMock.Verify(x =>
				x.LogError(It.Is(SameExceptionAs(expectedRegionValidationException))),
			Times.Once());
		
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
	
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
			_regionService.RetrieveRegionAsync(inputRegionId);

		// then
		Assert.ThrowsAsync<RegionValidationException>(() =>
			retrieveRegionTask.AsTask());

		_loggingBrokerMock.Verify(x =>
				x.LogError(It.Is(SameExceptionAs(expectedRegionValidationException))),
			Times.Once());
		
		_loggingBrokerMock.VerifyNoOtherCalls();
	}
}