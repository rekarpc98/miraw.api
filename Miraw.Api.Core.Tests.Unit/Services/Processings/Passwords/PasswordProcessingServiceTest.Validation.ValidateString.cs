using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Models.Passwords.Exceptions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Passwords;

public partial class PasswordProcessingServiceTest
{
	[Fact]
	public void ShouldThrowValidationExceptionOnInvalidInputStringAndLogIt()
	{
		// given 
		string shortRandomPasswordString = GetRandomPasswordString(4);
		string inputPasswordString = shortRandomPasswordString;
		
		var invalidPasswordException = new InvalidPasswordException();

		invalidPasswordException.UpsertDataList(
			key: "Password",
			value: "At least 5 characters expected");

		var expectedPasswordValidationException =
			new PasswordProcessingValidationException(invalidPasswordException);

		// when
		Action validatePasswordStringTask =
			() => passwordProcessingService.ValidatePasswordString(inputPasswordString);
		
		// then
		Assert.Throws<PasswordProcessingValidationException>(validatePasswordStringTask);

		loggingBroker.Verify(broker =>
				broker.LogError(It.Is(SameExceptionAs(expectedPasswordValidationException))),
			Times.Once);
	}
}