using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Users;

public partial class UserProcessingServiceTests
{
	[Fact]
	public async Task ShouldRegisterUserAsync()
	{
		// given
		User randomUser = CreateRandomUser();
		User inputUser = randomUser;
		User storageUser = inputUser;
		User expectedUser = storageUser.DeepClone();

		userServiceMock.Setup(x => x.RegisterUserAsync(inputUser)).ReturnsAsync(storageUser);

		// when
		User actualUser = await userProcessingService.RegisterUserAsync(inputUser);

		// then
		actualUser.Should().BeEquivalentTo(expectedUser);
		userServiceMock.Verify(x => x.RegisterUserAsync(inputUser), Times.Once);
		userServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}