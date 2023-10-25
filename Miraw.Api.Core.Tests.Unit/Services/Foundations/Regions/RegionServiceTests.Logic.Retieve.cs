using FluentAssertions;
using Force.DeepCloner;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Regions;

public partial class RegionServiceTests
{
	[Fact]
	public async Task ShouldRetrieveRegion()
	{
		// given

		var randomRegion = CreateRandomRegion();
		var reginId = randomRegion.Id;
		var storageRegion = randomRegion.DeepClone();
		var expectedRegion = storageRegion.DeepClone();

		_storageBrokerMock.Setup(x => x.SelectRegionByIdAsync(reginId))
			.ReturnsAsync(storageRegion);

		// when

		var actualRegion = await _regionService.RetrieveRegionAsync(reginId);

		// then

		actualRegion.Should().BeEquivalentTo(expectedRegion);

		_storageBrokerMock.Verify(broker =>
				broker.SelectRegionByIdAsync(reginId), Times.Once);

		_storageBrokerMock.VerifyNoOtherCalls();
	}
}