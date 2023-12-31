﻿using Miraw.Api.Core.Models.Processings.Regions;
using Miraw.Api.Core.Models.Regions;
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
	
	[Fact]
	public async Task ShouldThrowRegionProcessingValidationExceptionIfRegionIsNullAndLogItAsync()
	{
		// given
		Guid randomId = Guid.NewGuid();
		Guid inputRegionId = randomId;
		
		Region? nullRegion = null;
		Region? returnedRegion = nullRegion;
		
		var nullRegionException = new NullRegionException();
		
		var expectedRegionProcessingValidationException =
			new RegionProcessingDependencyValidationException(nullRegionException);

		
		regionServiceMock.Setup(x =>
				x.RetrieveRegionAsync(inputRegionId))!
			.ReturnsAsync(returnedRegion);
		
		// when
		ValueTask throwIfRegionNotExistsTask = regionProcessingService.ThrowIfRegionNotExistsAsync(inputRegionId);

		// then
		await Assert.ThrowsAsync<RegionProcessingDependencyValidationException>(() =>
			throwIfRegionNotExistsTask.AsTask());

		loggingBrokerMock.Verify(x =>
			x.LogError(It.Is(SameExceptionAs(expectedRegionProcessingValidationException))));
		
		regionServiceMock.Verify(x =>
				x.RetrieveRegionAsync(inputRegionId),
			Times.Once);
		
		regionServiceMock.VerifyNoOtherCalls();
		regionServiceMock.VerifyNoOtherCalls();
	}
}