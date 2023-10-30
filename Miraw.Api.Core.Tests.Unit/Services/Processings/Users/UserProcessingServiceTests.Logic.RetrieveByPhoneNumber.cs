
using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Users;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Users;

public partial class UserProcessingServiceTests
{
	[Fact]
	public async Task ShouldRetrieveUserByPhoneNumberAsync()
	{
		// given
		User randomUser = CreateRandomUser();
		User inputUser = randomUser;
		string inputPhoneNumber = inputUser.PhoneNumber;
		User storageUser = inputUser;
		User expectedUser = storageUser.DeepClone();

		userServiceMock.Setup(x =>
				x.RetrieveUserByPhoneNumberAsync(inputPhoneNumber))
			.ReturnsAsync(storageUser);

		// when
		User actualUser = await userProcessingService.RetrieveUserByPhoneNumberAsync(inputPhoneNumber);

		// then
		actualUser.Should().BeEquivalentTo(expectedUser);
		userServiceMock.Verify(x => x.RetrieveUserByPhoneNumberAsync(inputPhoneNumber), Times.Once);
		userServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}