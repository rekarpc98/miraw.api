using FluentAssertions;
using Miraw.Api.Core.Utilities;
using Miraw.Api.Core.Utilities.Securities;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Passwords;

public partial class PasswordProcessingServiceTest
{
	[Fact]
	public void ShouldHashPasswordStringEachTimeDespiteSameInputPasswordString()
	{
		// given
		string randomPasswordString = GetRandomPasswordString(7);
		string inputPasswordString = randomPasswordString;
		string expectedHashedPasswordString = SecurePasswordHasher.Hash(inputPasswordString);
		
		// when
		string actualHashedPasswordString =
			passwordProcessingService.HashPasswordString(inputPasswordString);

		// then
		actualHashedPasswordString.Should().NotBeEquivalentTo(expectedHashedPasswordString);
		
		passwordServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}