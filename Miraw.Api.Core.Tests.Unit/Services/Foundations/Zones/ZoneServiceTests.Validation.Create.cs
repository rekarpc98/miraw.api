using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Models.Zones.Exceptions;
using Moq;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Zones;

public partial class ZoneServiceTests
{
	[Fact]
	public void ShouldThrowZoneValidationExceptionOnCreateWhenZoneIsNullAndLogIt()
	{
		// given
		Zone? nullZone = null;
		var nullZoneException = new NullZoneException();

		var zoneValidationException = new ZoneValidationException(nullZoneException);

		// when
		ValueTask<Zone> createZoneTask = zoneService.CreateZoneAsync(nullZone!);

		// then
		Assert.ThrowsAsync<ZoneValidationException>(() => createZoneTask.AsTask());

		loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(zoneValidationException))),
			Times.Once);

		storageBrokerMock.Verify(broker =>
				broker.InsertZoneAsync(It.IsAny<Zone>()),
			Times.Never);

		storageBrokerMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ShouldThrowZoneValidationExceptionOnCreateWhenZoneIsInvalidAndLogIt()
	{
		// given
		Zone randomZone = CreateInvalidZone();
		Zone invalidZone = randomZone;

		var invalidZoneException = new InvalidZoneException();

		invalidZoneException.AddData(
			key: nameof(Zone.Id),
			values: "Invalid guid id");

		invalidZoneException.AddData(
			key: nameof(Zone.RegionId),
			values: "Invalid guid id");
		
		invalidZoneException.AddData(
			key: nameof(Zone.CreatedBy),
			values: "Invalid guid id");
		
		invalidZoneException.AddData(
			key: nameof(Zone.UpdatedBy),
			values: "Invalid guid id");
		
		invalidZoneException.AddData(
			key: nameof(Zone.Boundary),
			values: "Invalid zone boundary");
		
		invalidZoneException.AddData(
			key: nameof(Zone.CreatedDate),
			values: "Invalid date time offset");
		
		invalidZoneException.AddData(
			key: nameof(Zone.UpdatedDate),
			values: "Invalid date time offset");

		var zoneValidationException = new ZoneValidationException(invalidZoneException);

		// when
		ValueTask<Zone> createZoneTask = zoneService.CreateZoneAsync(invalidZone);

		// then
		Assert.ThrowsAsync<ZoneValidationException>(() => createZoneTask.AsTask());

		loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(zoneValidationException))),
			Times.Once);

		storageBrokerMock.Verify(broker =>
				broker.InsertZoneAsync(It.IsAny<Zone>()),
			Times.Never);

		storageBrokerMock.VerifyNoOtherCalls();
	}
}