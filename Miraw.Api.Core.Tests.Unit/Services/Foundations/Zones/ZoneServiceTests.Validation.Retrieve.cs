using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Models.Zones.Exceptions;
using Moq;

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
}