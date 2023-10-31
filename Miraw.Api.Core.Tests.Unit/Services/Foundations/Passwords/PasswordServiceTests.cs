using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Services.Foundations.Passwords;
using Moq;
using Tynamix.ObjectFiller;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Passwords;

public partial class PasswordServiceTests
{
	private readonly IPasswordService passwordService;
	private readonly Mock<IStorageBroker> storageBrokerMock = new();
	
	public PasswordServiceTests()
	{
	 	passwordService = new PasswordService(storageBrokerMock.Object);	
	}
	
	private static Password CreateRandomPassword()
	{
		return CreatePasswordFiller().Create();
	}

	private static Filler<Password> CreatePasswordFiller()
	{
		var filler = new Filler<Password>();

		filler.Setup()
			.OnType<DateTimeOffset>()
			.Use(CreateRandomDateTimeOffset())
			.OnType<DateTimeOffset?>()
			.IgnoreIt()
			.OnProperty(x => x.User)
			.IgnoreIt();

		return filler;
	}

	private static DateTimeOffset CreateRandomDateTimeOffset()
	{
		return DateTimeOffset.UtcNow;
	}
}