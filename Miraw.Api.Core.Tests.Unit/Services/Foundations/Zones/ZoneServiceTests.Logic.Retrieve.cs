using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Zones;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Zones;

public partial class ZoneServiceTests
{
	[Fact]
	public async Task ShouldRetrieveZoneById()
	{
		// given
		var randomId = Guid.NewGuid();
		Guid inputZoneId = randomId;
		Zone randomZone = CreateRandomZone(inputZoneId);
		Zone inputZone = randomZone;
		Zone storageZone = randomZone.DeepClone();
		Zone expectedZone = storageZone.DeepClone();

		storageBrokerMock.Setup(broker =>
				broker.SelectZoneByIdAsync(inputZoneId))
			.ReturnsAsync(storageZone);

		// when
		var actualZone = await zoneService.RetrieveZoneByIdAsync(inputZoneId); 
		
		// then
		actualZone.Should().BeEquivalentTo(expectedZone);
		storageBrokerMock.Verify(x => x.InsertZoneAsync(inputZone), Times.Once);
		storageBrokerMock.VerifyNoOtherCalls();
	}
}