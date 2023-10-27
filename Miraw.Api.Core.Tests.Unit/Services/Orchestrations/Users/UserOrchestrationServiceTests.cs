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

	private static User CreateInvalidUser(bool invalidEmail = true, bool invalidPhoneNumber = true)
	{
		var filler = new Filler<User>();

		filler.Setup()
			.OnProperty(x => x.CreatedDate)
			.Use(CreateRandomDateTime())
			.OnProperty(x => x.UpdatedDate)
			.Use(CreateRandomDateTime())
			.OnProperty(x => x.DeletedDate)
			.Use(CreateRandomDateTime())
			.OnProperty(x => x.Region)
			.IgnoreIt()
			.OnProperty(x => x.Email)
			.Use(invalidEmail ? string.Empty : new EmailAddresses(".com").ToString()!)
			.OnProperty(x => x.PhoneNumber)
			.Use(invalidPhoneNumber ? string.Empty : RandomDigits(13));

		return filler.Create();
	}

	private static string RandomDigits(int length)
	{
		var random = new Random();
		string s = string.Empty;
		for (int i = 0; i < length; i++)
			s = String.Concat(s, random.Next(10).ToString());
		return s;
	}

	private static DateTimeOffset CreateRandomDateTime()
	{
		return new DateTimeRange(earliestDate: new DateTime()).GetValue();
	}
}