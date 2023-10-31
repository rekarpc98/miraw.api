using FluentAssertions;
using Miraw.Api.Core.Utilities;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Passwords;

public partial class PasswordProcessingServiceTest
{
	[Fact]
	public void ShouldVerifyPasswordWithCorrespondingHash()
	{
		// given
		string randomString = GetRandomPasswordString(5);
		string inputPasswordString = randomString;
		string passwordHash = SecurePasswordHasher.Hash(inputPasswordString);

		// when
		Action verifyPasswordAction =
			() => passwordProcessingService.VerifyPasswordString(inputPasswordString, passwordHash);

		// then
		verifyPasswordAction.Should().NotThrow<Exception>();
		
		loggingBrokerMock.VerifyNoOtherCalls();
		passwordServiceMock.VerifyNoOtherCalls();
	}
}