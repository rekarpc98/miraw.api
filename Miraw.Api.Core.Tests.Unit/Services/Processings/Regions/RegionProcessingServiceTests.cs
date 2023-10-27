using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Services.Foundations.Regions;
using Miraw.Api.Core.Services.Processings.Regions;
using Moq;
using Tynamix.ObjectFiller;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Regions;

public partial class RegionProcessingServiceTests
{
	private IRegionProcessingService regionProcessingService;

	private readonly Mock<IRegionService> regionServiceMock = new();
	private readonly Mock<ILoggingBroker> loggingBrokerMock = new();

	public RegionProcessingServiceTests()
	{
		regionProcessingService = new RegionProcessingService(
			regionService: regionServiceMock.Object,
			loggingBroker: loggingBrokerMock.Object);
	}

	private static Region CreateRandomRegion(Guid? regionId = null)
	{
		return CreteRegionFiller(regionId).Create();
	}

	private static Filler<Region> CreteRegionFiller(Guid? regionId = null)
	{
		var filler = new Filler<Region>();

		filler.Setup()
			.OnProperty(x => x.Id)
			.Use(regionId ?? Guid.NewGuid())
			.OnProperty(x => x.Boundary)
			.IgnoreIt()
			.OnProperty(x => x.UpdatedDate)
			.IgnoreIt()
			.OnProperty(x => x.DeletedDate)
			.IgnoreIt()
			.OnProperty(x => x.CreatedDate)
			.IgnoreIt();

		return filler;
	}
}