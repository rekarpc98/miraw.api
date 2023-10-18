using System.Linq.Expressions;
using FluentAssertions;
using Force.DeepCloner;
using Miraw.Api.Core.Models.Regions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Regions;

public partial class RegionServiceTests
{
	[Fact]
	public async Task ShouldCreateRegionAsync()
	{
		// given
		Region randomRegion = CreateRandomRegion();
		Region inputRegion = randomRegion.DeepClone();
		Region storageRegion = randomRegion.DeepClone();
		Region expectedRegion = storageRegion.DeepClone();

		_storageBrokerMock.Setup(broker =>
			broker.InsertRegionAsync(inputRegion))
				.ReturnsAsync(storageRegion);

		// when
		Region actualRegion =
			await _regionService.CreateRegionAsync(inputRegion);

		// then
		actualRegion.Should().BeEquivalentTo(expectedRegion);

		_storageBrokerMock.Verify(broker =>
			broker.InsertRegionAsync(inputRegion),
				Times.Once);

		_storageBrokerMock.VerifyNoOtherCalls();
	}
}
