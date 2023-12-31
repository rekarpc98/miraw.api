﻿using System.Linq.Expressions;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Services.Foundations.Regions;
using Miraw.Api.Core.Services.Processings.Regions;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Regions;

public partial class RegionProcessingServiceTests
{
	private readonly IRegionProcessingService regionProcessingService;

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

	private static IQueryable<Region> CreateRandomRegions(int count = 5)
	{
		return CreteRegionFiller().Create(count).AsQueryable();
	}

	private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException) =>
		actualException => actualException.SameExceptionAs(expectedException);
}