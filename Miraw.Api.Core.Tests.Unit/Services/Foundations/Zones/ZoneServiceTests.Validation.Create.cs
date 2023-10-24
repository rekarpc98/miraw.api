using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Models.Zones.Exceptions;
using Moq;

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
	public void ShouldThrowZoneValidationExceptionOnCreateWhenIdIsInvalidAndLogIt()
	{
		// given
		Guid invalidId = Guid.Empty;
		Zone randomZone = CreateRandomZone(invalidId);
		Zone invalidZone = randomZone;

		var invalidZoneException = new InvalidZoneException();

		invalidZoneException.AddData(
			key: nameof(Zone.Id),
			values: "Invalid zone id");

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