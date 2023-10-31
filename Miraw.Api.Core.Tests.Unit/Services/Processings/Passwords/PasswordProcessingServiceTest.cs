using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Services.Foundations.Passwords;
using Miraw.Api.Core.Services.Processings.Passwords;
using Moq;
using Tynamix.ObjectFiller;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Passwords;

public partial class PasswordProcessingServiceTest
{
	private readonly IPasswordProcessingService passwordProcessingService;
	private readonly Mock<IPasswordService> passwordServiceMock = new();
	
	public PasswordProcessingServiceTest()
	{
		passwordProcessingService = new PasswordProcessingService(passwordServiceMock.Object);
	}
	
	private Password CreateRandomPassword()
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