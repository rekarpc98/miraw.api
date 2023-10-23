using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Zones;
using Moq;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Zones;

public partial class ZoneServiceTests
{
	[Fact]
	public async Task ShouldRetrieveZoneByIdAsync()
	{
		// given
		var randomId = Guid.NewGuid();
		Guid inputZoneId = randomId;
		Zone randomZone = CreateRandomZone(inputZoneId);
		Zone storageZone = randomZone.DeepClone();
		Zone expectedZone = storageZone.DeepClone();

		storageBrokerMock.Setup(broker =>
				broker.SelectZoneByIdAsync(inputZoneId))
			.ReturnsAsync(storageZone);

		// when
		var actualZone = await zoneService.RetrieveZoneByIdAsync(inputZoneId); 
		
		// then
		actualZone.Should().BeEquivalentTo(expectedZone);
		storageBrokerMock.Verify(x => x.SelectZoneByIdAsync(inputZoneId), Times.Once);
		storageBrokerMock.VerifyNoOtherCalls();
	}
	
	[Fact]
	public async Task ShouldRetrieveZoneByCoordinateAsync()
	{
		// given
		var randomCoordinate = new Point(1, 1);
		Point inputCoordinate = randomCoordinate;
		Geometry randomBoundary = CreateGeometryObject();
		Zone randomZone = CreateRandomZone(boundary: randomBoundary);
		Zone storageZone = randomZone.DeepClone();
		Zone expectedZone = storageZone.DeepClone();
		
		storageBrokerMock.Setup(broker =>
				broker.SelectZoneByCoordinateAsync(inputCoordinate))
			.ReturnsAsync(storageZone);
		
		// when

		Zone actualZone = await zoneService.RetrieveZoneByCoordinateAsync(inputCoordinate);

		// then
		
		actualZone.Should().BeEquivalentTo(expectedZone);
		storageBrokerMock.Verify(x => x.SelectZoneByCoordinateAsync(inputCoordinate), Times.Once);
		storageBrokerMock.VerifyNoOtherCalls();
	}
}