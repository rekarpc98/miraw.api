using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Passwords;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Passwords;

public partial class PasswordProcessingServiceTest
{
	[Fact]
	public async Task ShouldCreatePasswordAsync()
	{
		// given
		Password randomPassword = CreateRandomPassword();
		Password inputPassword = randomPassword;
		Password storagePassword = randomPassword;
		Password expectedPassword = storagePassword.DeepClone();

		passwordServiceMock.Setup(broker =>
				broker.CreatePasswordAsync(inputPassword))
			.ReturnsAsync(storagePassword);

		// when
		Password actualPassword = await passwordProcessingService.CreatePasswordAsync(inputPassword);

		// then
		actualPassword.Should().BeEquivalentTo(expectedPassword);

		passwordServiceMock.Verify(broker =>
				broker.CreatePasswordAsync(inputPassword),
			Times.Once);

		passwordServiceMock.VerifyNoOtherCalls();
	}
}