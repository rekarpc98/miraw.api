using System.Linq.Expressions;
using Miraw.Api.Core.Brokers.DateTimes;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Services.Orchestrations.Passwords;
using Miraw.Api.Core.Services.Processings.Passwords;
using Miraw.Api.Core.Services.Processings.Users;
using Miraw.Api.Core.Utilities;
using Miraw.Api.Core.Utilities.Securities;
using Moq;
using Tynamix.ObjectFiller;

namespace Miraw.Api.Core.Tests.Unit.Services.Orchestrations.Passwords;

public partial class PasswordOrchestrationServiceTests
{
	private readonly IPasswordOrchestrationService passwordOrchestrationService;
	private readonly Mock<IUserProcessingService> userProcessingServiceMock = new();
	private readonly Mock<IPasswordProcessingService> passwordProcessingServiceMock = new();
	private readonly Mock<ILoggingBroker> loggingBrokerMock = new();
	private readonly Mock<IDateTimeBroker> dateTimeBrokerMock = new();

	public PasswordOrchestrationServiceTests()
	{
		passwordOrchestrationService = new PasswordOrchestrationService(userProcessingServiceMock.Object,
			passwordProcessingServiceMock.Object, loggingBrokerMock.Object, dateTimeBrokerMock.Object);
	}

	private static Expression<Func<Password, bool>> SamePasswordAs(Password createdPassword) =>
		actualPassword => actualPassword.UserId == createdPassword.UserId &&
		                  actualPassword.PasswordHash == createdPassword.PasswordHash &&
		                  actualPassword.CreatedDate == createdPassword.CreatedDate;
						// actualPassword.Id == createdPassword.Id

	private static string CreateRandomString() => new MnemonicString().GetValue();

	private static string HashPasswordString(string inputPasswordString) =>
		SecurePasswordHasher.Hash(inputPasswordString);

	private static Password CreatePassword(Guid inputUserId, string hashedPasswordString,
		DateTimeOffset currentDateTime) =>
		new()
		{
			Id = Guid.NewGuid(),
			UserId = inputUserId,
			PasswordHash = hashedPasswordString,
			CreatedDate = currentDateTime
		};

	private static DateTimeOffset GetRandomDateTimeOffset()
	{
		return new DateTimeRange(earliestDate: new DateTime()).GetValue();
	}
}