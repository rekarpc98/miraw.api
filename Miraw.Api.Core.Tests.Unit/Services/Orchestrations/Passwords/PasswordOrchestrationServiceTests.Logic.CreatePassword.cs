using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Passwords;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Orchestrations.Passwords;

public partial class PasswordOrchestrationServiceTests
{
	[Fact]
	public async Task ShouldCreatePasswordForUserAsync()
	{
		// given
		Guid randomUserId = Guid.NewGuid();
		Guid inputUserId = randomUserId;
		DateTimeOffset randomCreatedDate = GetRandomDateTimeOffset();
		DateTimeOffset currentDateTime = randomCreatedDate;
		string randomPasswordString = CreateRandomString();
		string inputPasswordString = randomPasswordString;
		string hashedPasswordString = HashPasswordString(inputPasswordString);
		Password randomPassword = CreatePassword(inputUserId, hashedPasswordString, currentDateTime);
		Password createdPassword = randomPassword;
		Password expectedPassword = createdPassword.DeepClone();

		userProcessingServiceMock.Setup(x =>
				x.VerifyUserExistsAsync(inputUserId));
		
		dateTimeBrokerMock.Setup(x =>
				x.GetCurrentDateTime())
			.Returns(currentDateTime);
		
		passwordProcessingServiceMock.Setup(x =>
				x.HashPasswordString(inputPasswordString))
			.Returns(hashedPasswordString);
		
		passwordProcessingServiceMock.Setup(x =>
				x.CreatePasswordAsync(It.IsAny<Password>()))
			.ReturnsAsync(createdPassword);
		
		// when
		Password actualPassword =
			await passwordOrchestrationService.CreatePasswordForUserAsync(inputUserId, inputPasswordString);
		
		// then
		actualPassword.Should().BeEquivalentTo(expectedPassword);
		actualPassword.Id.Should().NotBe(Guid.Empty);
		
		userProcessingServiceMock.Verify(x =>
				x.VerifyUserExistsAsync(inputUserId),
			Times.Once);
		
		passwordProcessingServiceMock.Verify(x =>
				x.HashPasswordString(inputPasswordString),
			Times.Once);
		
		passwordProcessingServiceMock.Verify(x =>
				x.CreatePasswordAsync(It.Is(SamePasswordAs(createdPassword))),
			Times.Once);
		
		userProcessingServiceMock.VerifyNoOtherCalls();
		passwordProcessingServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}