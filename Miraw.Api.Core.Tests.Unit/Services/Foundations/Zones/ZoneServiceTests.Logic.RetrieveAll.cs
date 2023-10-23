using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Zones;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Zones;

public partial class ZoneServiceTests
{
	[Fact]
	public async Task ShouldRetrieveAllZonesAsync()
	{
		// given
		IQueryable<Zone> randomZones = CreateRandomZones();
		IQueryable<Zone> storageZones = randomZones.DeepClone();
		IQueryable<Zone> expectedZones = storageZones.DeepClone();

		storageBrokerMock.Setup(x => x.SelectAllZones()).Returns(storageZones);

		// when
		IQueryable<Zone> actualZones = await zoneService.RetrieveAllZonesAsync();

		// then
		actualZones.Should().BeEquivalentTo(expectedZones);
		storageBrokerMock.Verify(x => x.SelectAllZones(), Times.Once);
		storageBrokerMock.VerifyNoOtherCalls();
	}
}