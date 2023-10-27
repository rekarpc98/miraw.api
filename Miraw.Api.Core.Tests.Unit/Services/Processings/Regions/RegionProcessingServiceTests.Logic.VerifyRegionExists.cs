
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
		var randomRegionId = Guid.NewGuid();
		Guid inputRegionId = randomRegionId;
		Region randomRegion = CreateRandomRegion(randomRegionId);
		Region storageRegion = randomRegion;
		const bool expected = true;
		
		regionServiceMock.Setup(x => x.RetrieveRegionAsync(inputRegionId))
			.ReturnsAsync(storageRegion);
		
		// when
		bool actual = await regionProcessingService.VerifyRegionExistsAsync(inputRegionId);
		
		// then
		
		actual.Should().Be(expected);
		
		regionServiceMock.Verify(x => 
				x.RetrieveRegionAsync(inputRegionId),
			Times.Once);
		
		loggingBrokerMock.Verify(x => x.LogError(It.IsAny<Exception>()), Times.Never);
		
		regionServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
	
	[Fact]
	public async Task ShouldReturnFalseIfRegionNotExists()
	{
		// given
		var randomRegionId = Guid.NewGuid();
		Guid inputRegionId = randomRegionId;
		Region nullRegion = null!;
		Region storageRegion = nullRegion;
		const bool expected = false;
		
		regionServiceMock.Setup(x => x.RetrieveRegionAsync(inputRegionId))
			.ReturnsAsync(storageRegion);
		
		// when
		bool actual = await regionProcessingService.VerifyRegionExistsAsync(inputRegionId);
		
		// then
		
		actual.Should().Be(expected);
		
		regionServiceMock.Verify(x => 
				x.RetrieveRegionAsync(inputRegionId),
			Times.Once);
		
		loggingBrokerMock.Verify(x => x.LogError(It.IsAny<Exception>()), Times.Never);
		
		regionServiceMock.VerifyNoOtherCalls();
		loggingBrokerMock.VerifyNoOtherCalls();
	}
}