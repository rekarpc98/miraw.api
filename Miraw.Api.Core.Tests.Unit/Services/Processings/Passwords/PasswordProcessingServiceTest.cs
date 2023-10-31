using System.Linq.Expressions;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Models.Passwords.Exceptions;
using Miraw.Api.Core.Services.Foundations.Passwords;
using Miraw.Api.Core.Services.Processings.Passwords;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Passwords;

public partial class PasswordProcessingServiceTest
{
	private readonly IPasswordProcessingService passwordProcessingService;
	private readonly Mock<IPasswordService> passwordServiceMock = new();
	private readonly Mock<ILoggingBroker> loggingBrokerMock = new();

	public PasswordProcessingServiceTest()
	{
		passwordProcessingService = new PasswordProcessingService(passwordServiceMock.Object, loggingBrokerMock.Object);
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

	private static string GetRandomPasswordString(int count)
	{
		string passwordString = new MnemonicString(count).GetValue();
		return passwordString.Substring(0, count);
	}

	private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException) =>
		actualException => actualException.SameExceptionAs(expectedException);
}