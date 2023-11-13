using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Services.Orchestrations.Auths;
using Miraw.Api.Core.Services.Processings.Passwords;
using Miraw.Api.Core.Services.Processings.Users;
using Miraw.Api.Core.Utilities.Securities;
using Moq;
using Tynamix.ObjectFiller;

namespace Miraw.Api.Core.Tests.Unit.Services.Orchestrations.Auths;

public partial class AuthOrchestrationServiceTests
{
	private readonly IAuthOrchestrationService authOrchestrationService;
	private readonly Mock<IPasswordProcessingService> passwordProcessingServiceMock = new();
	private readonly Mock<IUserProcessingService> userProcessingServiceMock = new();
	private readonly Mock<ILoggingBroker> loggingBrokerMock = new();
	private readonly Mock<IJwtTokenGenerator> jwtTokenGeneratorMock = new();

	public AuthOrchestrationServiceTests()
	{
		authOrchestrationService = new AuthOrchestrationService(
			passwordProcessingServiceMock.Object,
			userProcessingServiceMock.Object,
			loggingBrokerMock.Object,
			jwtTokenGeneratorMock.Object
		);
	}

	private static User CreateRandomUser()
	{
		return CreateRandomUserFiller().Create();
	}

	private static Filler<User> CreateRandomUserFiller()
	{
		var filler = new Filler<User>();

		filler.Setup()
			.OnProperty(user => user.Id)
			.Use(Guid.NewGuid)
			.OnProperty(user => user.Name)
			.Use(Guid.NewGuid().ToString())
			.OnProperty(user => user.Region)
			.IgnoreIt()
			.OnType<DateTimeOffset>()
			.IgnoreIt()
			.OnType<DateTimeOffset?>()
			.IgnoreIt();
		
		return filler;
	}
}