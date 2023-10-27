
using FluentAssertions;
using Miraw.Api.Core.Models.Regions;
using Moq;

namespace Miraw.Api.Core.Tests.Unit.Services.Processings.Regions;

public partial class RegionProcessingServiceTests
{
	[Fact]
	public async Task ShouldReturnTrueIfRegionExists()
	{
		// given
		IQueryable<Region> randomRegions = CreateRandomRegions();
		IQueryable<Region> storageRegions = randomRegions;
		Guid inputRegionId = randomRegions.First().Id;
		
		const bool expectedValue = true;
		
		regionServiceMock.Setup(x => x.RetrieveAllRegionsAsync())
			.ReturnsAsync(storageRegions);
		
		// when
		bool actual = await regionProcessingService.VerifyRegionExistsAsync(inputRegionId);
		
		// then
		actual.Should().Be(expectedValue);
		
		regionServiceMock.Verify(x => 
				x.RetrieveAllRegionsAsync(),
			Times.Once);
		
		loggingBrokerMock.Verify(x => x.LogError(It.IsAny<Exception>()), Times.Never);
		
		regionServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
	
	[Fact]
	public async Task ShouldReturnFalseIfRegionNotExists()
	{
		// given
		IQueryable<Region> randomRegions = CreateRandomRegions();
		IQueryable<Region> storageRegions = randomRegions;
		Guid inputRegionId = Guid.NewGuid();
		
		const bool expectedValue = false;
		
		regionServiceMock.Setup(x => x.RetrieveAllRegionsAsync())
			.ReturnsAsync(storageRegions);
		
		// when
		bool actual = await regionProcessingService.VerifyRegionExistsAsync(inputRegionId);
		
		// then
		actual.Should().Be(expectedValue);
		
		regionServiceMock.Verify(x => 
				x.RetrieveAllRegionsAsync(),
			Times.Once);
		
		loggingBrokerMock.Verify(x => x.LogError(It.IsAny<Exception>()), Times.Never);
		
		regionServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}