using System.Linq.Expressions;
using Miraw.Api.Core.Brokers.DateTimes;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Services.Foundations.Users;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Users;

public partial class UserServiceTests
{
	private readonly Mock<IStorageBroker> storageBrokerMock = new();
	private readonly Mock<IDateTimeBroker> dateTimeBrokerMock = new();
	private readonly Mock<ILoggingBroker> loggingBrokerMock = new();
	private readonly IUserService userService;

	public UserServiceTests()
	{
		userService = new UserService(
			storageBroker: storageBrokerMock.Object,
			dateTimeBroker: dateTimeBrokerMock.Object,
			loggingBroker: loggingBrokerMock.Object);
	}

	private static DateTimeOffset GetRandomDateTime() => new DateTimeRange(earliestDate: new DateTime()).GetValue();

	private static User CreateRandomUser() => CreateUserFiller(DateTimeOffset.UtcNow).Create();
	private static User CreateRandomUser(DateTimeOffset date) => CreateUserFiller(date).Create();
	private static User CreateRandomUser(DateTimeOffset date, string name) => CreateUserFiller(date, name).Create();

	private static IQueryable<User> CreateRandomUsers(DateTimeOffset date)
	{
		var users = new List<User>();
		for (int i = 0; i < GetRandomNumber(); i++)
		{
			users.Add(CreateUserFiller(date).Create());
		}

		return users.AsQueryable();
	}

	private static Filler<User> CreateUserFiller(DateTimeOffset date)
	{
		var filler = new Filler<User>();
		Guid userId = Guid.NewGuid();
		Guid regionId = Guid.NewGuid();

		var randomNumber = Random.Shared.Next(0, 1);
		var randomGender = (Gender)randomNumber;

		filler.Setup()
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
			.Use(new RealNames(NameStyle.FirstName))
			.OnProperty(x => x.Email)
			.Use(new EmailAddresses())
			.OnProperty(x => x.PhoneNumber)
			.Use(new PatternGenerator("{N:13}"))
			.OnProperty(x => x.RegionId)
			.Use(regionId)
			.OnProperty(x => x.Gender)
			.Use(randomGender)
			.OnProperty(x => x.Region)
			.IgnoreIt()
			.OnType<DateTimeOffset>().Use(GetRandomDateTime());

		return filler;
	}

	private static Filler<User> CreateUserFiller(DateTimeOffset date, string name)
	{
		var filler = new Filler<User>();
		Guid userId = Guid.NewGuid();
		Guid regionId = Guid.NewGuid();

		var randomNumber = Random.Shared.Next(0, 1);
		var randomGender = (Gender)randomNumber;

		filler.Setup()
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
			.Use(name)
			.OnProperty(x => x.Email)
			.Use(new EmailAddresses())
			.OnProperty(x => x.PhoneNumber)
			.Use(new PatternGenerator("{N:13}"))
			.OnProperty(x => x.RegionId)
			.Use(regionId)
			.OnProperty(x => x.Gender)
			.Use(randomGender)
			.OnProperty(x => x.Region)
			.IgnoreIt();

		return filler;
	}

	private static int GetRandomNumber() => new IntRange(min: 2, max: 90).GetValue();

	private static Expression<Func<Xeption, bool>> SameExceptionAs(Exception expectedUserValidationException)
	{
		return actualException => actualException.SameExceptionAs(expectedUserValidationException);
	}
}