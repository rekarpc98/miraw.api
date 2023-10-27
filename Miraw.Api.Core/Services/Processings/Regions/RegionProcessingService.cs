using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Models.Orchestrations.Processings.Regions;
using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Regions.Exceptions;
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
		try
		{
			Region maybeRegion = await regionService.RetrieveRegionAsync(regionId);

			ValidateStorageRegion(maybeRegion);
		}
		catch (NullRegionException nullRegionException)
		{
			var regionProcessingValidationException =
				new RegionProcessingDependencyValidationException(nullRegionException);

			loggingBroker.LogError(regionProcessingValidationException);

			throw regionProcessingValidationException;
		}
		catch (RegionValidationException regionValidationException)
		{
			Exception? innerRegionValidationException = regionValidationException.InnerException;

			RegionProcessingDependencyValidationException regionProcessingDependencyValidationException =
				innerRegionValidationException is not null
					? new RegionProcessingDependencyValidationException(innerRegionValidationException)
					: new RegionProcessingDependencyValidationException();

			loggingBroker.LogError(regionProcessingDependencyValidationException);

			throw regionProcessingDependencyValidationException;
		}
	}

	private static void ValidateStorageRegion(Region? maybeRegion)
	{
		if (maybeRegion is null)
		{
			throw new NullRegionException();
		}
	}
}