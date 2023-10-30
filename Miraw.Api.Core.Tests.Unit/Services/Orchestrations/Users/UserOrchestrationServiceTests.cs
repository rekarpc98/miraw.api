using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
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
			.OnType<DateTimeOffset>()
			.Use(GetRandomDateTime())
			.OnType<DateTimeOffset?>()
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
			.OnType<DateTimeOffset>()
			.Use(GetRandomDateTime())
			.OnType<DateTimeOffset?>()
			.IgnoreIt();

		return filler;
	}

	private static DateTimeOffset GetRandomDateTime()
	{
		return new DateTimeRange(earliestDate: new DateTime()).GetValue();
	}

	private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
	{
		return actualException => expectedException.SameExceptionAs(expectedException);
	}

	private static User CreateInvalidUser(bool invalidEmail = true, bool invalidPhoneNumber = true)
	{
		var filler = new Filler<User>();

		filler.Setup()
			.OnType<DateTimeOffset>()
			.Use(GetRandomDateTime())
			.OnType<DateTimeOffset?>()
			.IgnoreIt()
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

	private static string GetRandomString()
	{
		return new MnemonicString().GetValue();
	}

	private static string HashString(string text)
    {
        byte[] hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(text));

        var sb = new StringBuilder();
        foreach (byte b in hashedBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }

}