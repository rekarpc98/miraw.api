using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Services.Foundations.Regions;

namespace Miraw.Api.Core.Services.Processings.Regions;

public class RegionProcessingService : IRegionProcessingService
{
	private readonly IRegionService regionService;
	private readonly ILoggingBroker loggingBroker;

	public RegionProcessingService(IRegionService regionService, ILoggingBroker loggingBroker)
	{
		this.regionService = regionService;
		this.loggingBroker = loggingBroker;
	}
	public async ValueTask<bool> VerifyRegionExistsAsync(Guid regionId)
	{
		await regionService.RetrieveRegionAsync(regionId);
		return true;
	}
}