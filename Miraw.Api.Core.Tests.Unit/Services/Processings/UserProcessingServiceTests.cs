
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Services.Foundations.Users;
using Miraw.Api.Core.Services.Processings.Users;
using Moq;
using Tynamix.ObjectFiller;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings;

public partial class UserProcessingServiceTests
{
	private readonly IUserProcessingService userProcessingService;
	private readonly Mock<IUserService> userServiceMock = new();

	public UserProcessingServiceTests()
	{
		userProcessingService = new UserProcessingService(userServiceMock.Object);
	}

	private static IQueryable<User> CreateRandomUsers(DateTimeOffset? date = null)
	{
		var users = new List<User>();
		for (var i = 0; i < GetRandomNumber(); i++)
		{
			users.Add(CreateUserFiller(date ?? DateTimeOffset.UtcNow).Create());
		}

		return users.AsQueryable();
	}

	private static int GetRandomNumber()
	{
		var random = new Random();
		return random.Next(0, 10);
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
			.IgnoreIt();

		return filler;
	}
}