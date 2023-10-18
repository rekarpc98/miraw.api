using System.Linq.Expressions;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Users.Exceptions;
using Miraw.Api.Core.Services.Foundations.Regions;
using Moq;
using NetTopologySuite.Geometries;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Regions;

public partial class RegionServiceTests
{
	readonly Mock<IStorageBroker> _storageBrokerMock;
	readonly Mock<ILoggingBroker> _loggingBrokerMock;
	readonly IRegionService _regionService;

	public RegionServiceTests()
	{
		_storageBrokerMock = new Mock<IStorageBroker>();
		_loggingBrokerMock = new Mock<ILoggingBroker>();
		
		_regionService = new RegionService(_storageBrokerMock.Object, _loggingBrokerMock.Object);
	}

	static Region CreateRandomRegion()
	{
		Filler<Region> filler = CreateRegionFiller(DateTimeOffset.UtcNow);
		
		return filler.Create();
	}
	
	static Filler<Region> CreateRegionFiller(DateTimeOffset date)
	{
		var filler = new Filler<Region>();
		Guid userId = Guid.NewGuid();
		Guid regionId = Guid.NewGuid();

		filler.Setup()
			.OnProperty(x => x.Id)
			.Use(regionId)
			.OnProperty(x => x.CreatedDate)
			.Use(date)
			.OnProperty(x => x.UpdatedDate)
			.Use(date)
			.OnProperty(x => x.DeletedDate)
			.IgnoreIt()
			.OnProperty(x => x.DeletedBy)
			.IgnoreIt()
			.OnProperty(x => x.CreatedBy)
			.Use(userId)
			.OnProperty(x => x.UpdatedBy)
			.Use(userId)
			.OnProperty(x => x.Name)
			.Use(new MnemonicString().GetValue())
			.OnProperty(x => x.Boundary)
			.Use(new Polygon(new LinearRing(new[]
			{
				new Coordinate(0, 0),
				new Coordinate(0, 1),
				new Coordinate(1, 1),
				new Coordinate(1, 0),
				new Coordinate(0, 0)
			})));

		return filler;
	}

	static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedUserValidationException)
	{
		return actualException => actualException.SameExceptionAs(expectedUserValidationException);
	}
}