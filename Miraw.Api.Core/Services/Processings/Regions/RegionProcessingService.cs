using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Regions;
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
		IQueryable<Region> regions = await regionService.RetrieveAllRegionsAsync();
		return regions.Any(region => region.Id == regionId);
	}

	public async ValueTask ThrowIfRegionNotExistsAsync(Guid regionId)
	{
		throw new NotImplementedException();
	}
}