using FluentAssertions;
using Force.DeepCloner;
using Microsoft.Extensions.DependencyInjection;
using Miraw.Api.Core.Models.Passwords;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Passwords;

public partial class PasswordServiceTests
{
	[Fact]
	public async Task ShouldCreatePassword()
	{
		// given
		Password randomPassword = CreateRandomPassword();
		Password inputPassword = randomPassword;
		Password storagePassword = inputPassword;
		Password expectedPassword = storagePassword.DeepClone();
		
		storageBrokerMock.Setup(x => 
				x.InsertPasswordAsync(inputPassword))
			.ReturnsAsync(storagePassword);
		
		// when
		Password actualPassword = await passwordService.CreatePasswordAsync(inputPassword);
		
		// then
		
		actualPassword.Should().BeEquivalentTo(expectedPassword);
		storageBrokerMock.Verify(x => x.InsertPasswordAsync(inputPassword), Times.Once);
		storageBrokerMock.VerifyNoOtherCalls();
	}
}