using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Models.Zones.Exceptions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Zones;

public partial class ZoneServiceTests
{
	[Fact]
	public void ShouldThrowZoneValidationExceptionOnCreateWhenZoneIsNullAndLogItAsync()
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
	public async Task ShouldThrowZoneValidationExceptionOnCreateWhenZoneIsInvalidAndLogIt()
	{
		// given
		Zone randomZone = CreateInvalidZone();
		Zone invalidZone = randomZone;
		var invalidZoneException = new InvalidZoneException();

		invalidZoneException.UpsertDataList(
			key: nameof(Zone.Id),
			value: "Invalid guid id");

		invalidZoneException.UpsertDataList(
			key: nameof(Zone.RegionId),
			value: "Invalid guid id");

		invalidZoneException.UpsertDataList(
			key: nameof(Zone.CreatedBy),
			value: "Invalid guid id");

		invalidZoneException.UpsertDataList(
			key: nameof(Zone.UpdatedBy),
			value: "Invalid guid id");

		invalidZoneException.UpsertDataList(
			key: nameof(Zone.Boundary),
			value: "Invalid zone boundary");

		invalidZoneException.UpsertDataList(
			key: nameof(Zone.CreatedDate),
			value: "Invalid date time offset");

		invalidZoneException.UpsertDataList(
			key: nameof(Zone.UpdatedDate),
			value: "Invalid date time offset");

		var zoneValidationException = new ZoneValidationException(invalidZoneException);

		// when
		ValueTask<Zone> createZoneTask = zoneService.CreateZoneAsync(invalidZone);

		// then
		await Assert.ThrowsAsync<ZoneValidationException>(() => createZoneTask.AsTask());

		loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(zoneValidationException))),
			Times.Once);

		storageBrokerMock.Verify(broker =>
				broker.InsertZoneAsync(It.IsAny<Zone>()),
			Times.Never);

		storageBrokerMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ShouldThrowZoneValidationExceptionOnCreateWhenUpdatedDateAndCreatedDateAreNotSameAndLogItAsync()
	{
		// given
		DateTimeOffset firstRandomDateTimeOffset = GetRandomDateTime();
		DateTimeOffset secondRandomDateTimeOffset = GetRandomDateTime();

		Zone randomZone =
			CreateRandomZone(createdDate: firstRandomDateTimeOffset, updatedDate: secondRandomDateTimeOffset);
		
		Zone invalidZone = randomZone;
		var invalidZoneException = new InvalidZoneException();

		invalidZoneException.UpsertDataList(
			key: nameof(Zone.UpdatedDate),
			value: $"Date is not the same as {nameof(Zone.CreatedDate)}");

		var zoneValidationException = new ZoneValidationException(invalidZoneException);

		// when
		ValueTask<Zone> createZoneTask = zoneService.CreateZoneAsync(invalidZone);

		// then
		await Assert.ThrowsAsync<ZoneValidationException>(() => createZoneTask.AsTask());

		loggingBrokerMock.Verify(x =>
			x.LogError(It.Is(SameExceptionAs(zoneValidationException))), Times.Once);

		storageBrokerMock.Verify(broker =>
				broker.InsertZoneAsync(It.IsAny<Zone>()),
			Times.Never);

		storageBrokerMock.VerifyNoOtherCalls();
	}
}