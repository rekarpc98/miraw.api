using System.Linq.Expressions;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Services.Orchestrations.Users;
using Miraw.Api.Core.Services.Processings.Regions;
using Miraw.Api.Core.Services.Processings.Users;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Miraw.Api.Core.Tests.Unit.Services.Orchestrations.Users;

public partial class UserOrchestrationServiceTests
{
	private readonly IUserOrchestrationService userOrchestrationService;
	private readonly Mock<IUserProcessingService> userProcessingServiceMock = new();
	private readonly Mock<IRegionProcessingService> regionProcessingServiceMock = new();
	private readonly Mock<ILoggingBroker> loggingBrokerMock = new();

	public UserOrchestrationServiceTests()
	{
		userOrchestrationService =
			new UserOrchestrationService(userProcessingServiceMock.Object, regionProcessingServiceMock.Object,
				loggingBrokerMock.Object);
	}

	private static Region CreateRandomRegion(Guid? regionId = null)
	{
		return CreateRegionFiller(regionId ?? Guid.NewGuid()).Create();
	}

	private static Filler<Region> CreateRegionFiller(Guid regionId)
	{
		var filler = new Filler<Region>();

		filler.Setup()
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

	private static User CreateRandomUser(Guid? regionId = null)
	{
		return CreateUserFiller(regionId ?? Guid.NewGuid()).Create();
	}

	private static Filler<User> CreateUserFiller(Guid regionId)
	{
		var filler = new Filler<User>();

		filler.Setup()
			.OnProperty(x => x.RegionId)
			.Use(regionId)
			.OnProperty(x => x.Region)
			.IgnoreIt()
			.OnProperty(x => x.UpdatedDate)
			.IgnoreIt()
			.OnProperty(x => x.DeletedDate)
			.IgnoreIt()
			.OnProperty(x => x.CreatedDate)
			.IgnoreIt();

		return filler;
	}

	private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
	{
		return actualException => expectedException.SameExceptionAs(expectedException);
	}
}