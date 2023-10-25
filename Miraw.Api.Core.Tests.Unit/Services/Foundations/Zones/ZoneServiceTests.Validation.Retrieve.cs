﻿using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Models.Zones.Exceptions;
using Moq;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Zones;

public partial class ZoneServiceTests
{
	[Fact]
	public async Task ShouldThrowZoneValidationExceptionOnRetrieveByIdWhenInputIdIsInvalidAndLogItAsync()
	{
		// given
		var invalidId = Guid.Empty;
		Guid inputId = invalidId;

		var invalidZoneException = new InvalidZoneException(nameof(Zone.Id), inputId);
		var expectedZoneValidationException = new ZoneValidationException(invalidZoneException);

		storageBrokerMock.Setup(broker =>
				broker.SelectZoneByIdAsync(invalidId))
			.ThrowsAsync(invalidZoneException);

		// when
		ValueTask<Zone> retrieveZoneByIdTask = zoneService.RetrieveZoneByIdAsync(inputId);

		// then
		await Assert.ThrowsAsync<ZoneValidationException>(() => retrieveZoneByIdTask.AsTask());

		storageBrokerMock.Verify(broker =>
				broker.SelectZoneByIdAsync(invalidId),
			Times.Never);

		loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(expectedZoneValidationException))),
			Times.Once);

		storageBrokerMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ShouldThrowValidationExceptionOnRetrieveByIdWhenStorageZoneIsNullAndLogItAsync()
	{
		// given
		var randomGuid = Guid.NewGuid();
		Zone? nullZone = null;
		Zone? storageZone = nullZone;
		var notFoundZoneException = new NotFoundZoneException(randomGuid);
		var expectedZoneValidationException = new ZoneValidationException(notFoundZoneException);

		storageBrokerMock.Setup(x =>
				x.SelectZoneByIdAsync(randomGuid))
			.ReturnsAsync(storageZone);

		// when
		ValueTask<Zone> retrieveZoneByIdTask = zoneService.RetrieveZoneByIdAsync(randomGuid);

		// then
		await Assert.ThrowsAsync<ZoneValidationException>(() => retrieveZoneByIdTask.AsTask());

		storageBrokerMock.Verify(x => x.SelectZoneByIdAsync(randomGuid), Times.Once);

		storageBrokerMock.VerifyNoOtherCalls();

		loggingBrokerMock.Verify(x =>
				x.LogError(It.Is(SameExceptionAs(expectedZoneValidationException))),
			Times.Once);
	}

	[Fact]
	public async Task ShouldThrowValidationExceptionOnRetrieveByCoordinateWhenInputCoordinateIsInvalidAndLogItAsync()
	{
		// given
		var invalidCoordinate = new Point(0, 0);
		Point inputCoordinate = invalidCoordinate;
		
		var invalidZoneException = new InvalidZoneException("Coordinate", inputCoordinate);
		var expectedZoneValidationException = new ZoneValidationException(invalidZoneException);
		
		// when
		ValueTask<Zone> retrieveZoneByIdTask = zoneService.RetrieveZoneByCoordinateAsync(inputCoordinate);

		// then
		await Assert.ThrowsAsync<ZoneValidationException>(() => retrieveZoneByIdTask.AsTask());

		storageBrokerMock.Verify(x => x.SelectZoneByCoordinateAsync(inputCoordinate), Times.Once);

		storageBrokerMock.VerifyNoOtherCalls();

		loggingBrokerMock.Verify(x =>
				x.LogError(It.Is(SameExceptionAs(expectedZoneValidationException))),
			Times.Once);
	}
}