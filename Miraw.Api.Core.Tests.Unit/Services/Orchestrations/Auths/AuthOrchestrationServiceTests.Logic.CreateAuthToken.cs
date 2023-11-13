using FluentAssertions;
using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Orchestrations.Auths;

public partial class AuthOrchestrationServiceTests
{
	[Fact]
	public async Task ShouldCreateAuthTokenForUserAsync()
	{
		// given
		User randomUser = CreateRandomUser();
		User storageUser = randomUser;
		Guid inputUserId = randomUser.Id;
		var randomToken = Guid.NewGuid().ToString();
		string expectedToken = randomToken;

		userProcessingServiceMock.Setup(service =>
				service.RetrieveUserByIdAsync(inputUserId))
			.ReturnsAsync(storageUser);

		jwtTokenGeneratorMock.Setup(x => x.GenerateTokenForUser(storageUser))
			.Returns(randomToken);

		// when
		string actualToken = await authOrchestrationService.CreateAuthTokenForUserAsync(inputUserId);

		// then
		actualToken.Should().BeEquivalentTo(expectedToken);

		userProcessingServiceMock.Verify(service =>
				service.RetrieveUserByIdAsync(inputUserId),
			Times.Once);

		jwtTokenGeneratorMock.Verify(x =>
				x.GenerateTokenForUser(storageUser),
			Times.Once);
		
		userProcessingServiceMock.VerifyNoOtherCalls();
		jwtTokenGeneratorMock.VerifyNoOtherCalls();
		passwordProcessingServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}