using Miraw.Api.Core.Models.Passwords.Exceptions;
using Miraw.Api.Core.Utilities;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Passwords;

public partial class PasswordProcessingServiceTest
{
	[Fact]
	public void ShouldThrowValidationExceptionOnVerifyPasswordWhenPasswordAndHashedPasswordMismatch()
	{
		// given
		string firstRandomString = GetRandomPasswordString(5);
		string inputPasswordString = firstRandomString;
		string secondRandomString = GetRandomPasswordString(5);
		string incorrectInputPasswordString = secondRandomString;
		string passwordHash = SecurePasswordHasher.Hash(inputPasswordString);

		var incorrectPasswordException = new IncorrectPasswordException();
		var expectedPasswordProcessingValidationException =
			new PasswordProcessingValidationException(incorrectPasswordException);

		// when
		Action verifyPasswordAction =
			() => passwordProcessingService.VerifyPasswordString(incorrectInputPasswordString, passwordHash);

		// then
		Assert.Throws<PasswordProcessingValidationException>(verifyPasswordAction);

		loggingBrokerMock.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(expectedPasswordProcessingValidationException))),
			Times.Once);

		loggingBrokerMock.VerifyNoOtherCalls();
		passwordServiceMock.VerifyNoOtherCalls();
	}
}