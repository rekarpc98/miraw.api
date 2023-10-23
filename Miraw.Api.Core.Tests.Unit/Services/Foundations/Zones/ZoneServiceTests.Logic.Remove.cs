using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Zones;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Zones;

public partial class ZoneServiceTests
{
	[Fact]
	public async Task ShouldRemoveZoneAsync()
	{
		// given
		Zone randomZone = CreateRandomZone();
		Zone inputZone = randomZone;
		Zone storageZone = randomZone.DeepClone();
		Zone expectedZone = storageZone.DeepClone();

		storageBrokerMock.Setup(broker =>
				broker.DeleteZoneAsync(inputZone))
			.ReturnsAsync(storageZone);

		// when
		var actualZone = await zoneService.RemoveZoneAsync(inputZone); 
		
		// then
		actualZone.Should().BeEquivalentTo(expectedZone);
		storageBrokerMock.Verify(x => x.DeleteZoneAsync(inputZone), Times.Once);
		storageBrokerMock.VerifyNoOtherCalls();
		
	}
}