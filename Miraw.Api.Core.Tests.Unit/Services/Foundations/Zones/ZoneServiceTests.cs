using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Services.Foundations.Zones;
using Moq;
using NetTopologySuite.Geometries;
using Tynamix.ObjectFiller;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Zones;

public partial class ZoneServiceTests
{
	private readonly Mock<IStorageBroker> storageBrokerMock = new();

	private readonly IZoneService zoneService;

	public ZoneServiceTests()
	{
		zoneService = new ZoneService(storageBroker: storageBrokerMock.Object);
	}

	//private static Zone CreateRandomZone() => CreateZoneFiller().Create();
	private static Zone CreateRandomZone() => new()
	{
		Id = Guid.NewGuid(),
		RegionId = Guid.NewGuid(),
		Boundary = CreateGeometryObject()
	};

	private static Filler<Zone> CreateZoneFiller()
	{
		var filler = new Filler<Zone>();

		filler.Setup()
			.OnProperty(x => x.Boundary)
			.Use(() => CreateGeometryObject())
			.OnType<DateTimeOffset>()
			.Use(new DateTimeOffset())
			.OnType<DateTimeOffset?>()
			.IgnoreIt();

		return filler;
	}
	
	private static Geometry CreateGeometryObject()
	{
		var coordinates = new LinearRing(new[]
		{
			new Coordinate(0, 0),
			new Coordinate(0, 1),
			new Coordinate(1, 1),
			new Coordinate(1, 0),
			new Coordinate(0, 0)
		});

		return new Polygon(coordinates);
	}
}