using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Zones;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Zones;

public partial class ZoneServiceTests
{
	[Fact]
	public async Task ShouldModifyZoneAsync()
	{
		// given
		Zone randomZone = CreateRandomZone();
		Zone inputZone = randomZone;
		Zone storageZone = randomZone.DeepClone();
		Zone expectedZone = storageZone.DeepClone();

		storageBrokerMock.Setup(broker =>
				broker.UpdateZoneAsync(inputZone))
			.ReturnsAsync(storageZone);

		// when
		var actualZone = await zoneService.ModifyZoneAsync(inputZone); 
		
		// then
		actualZone.Should().BeEquivalentTo(expectedZone);
		storageBrokerMock.Verify(x => x.UpdateZoneAsync(inputZone), Times.Once);
		storageBrokerMock.VerifyNoOtherCalls();
	}
}