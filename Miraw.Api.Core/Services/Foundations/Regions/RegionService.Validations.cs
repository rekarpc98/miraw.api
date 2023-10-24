using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Regions.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Regions;

public partial class RegionService
{
	private static void ValidateRegionOnCreate(Region? region)
	{
		ValidateRegion(region);
	}

	private static void ValidateRegion(Region? region)
	{
		if (region is null)
		{
			throw new NullRegionException();
		}
	}

	private static void ValidateRegionId(Guid regionId)
	{
		if (regionId == Guid.Empty)
		{
			throw new InvalidRegionException(parameterName: nameof(Region.Id), parameterValue: regionId);
		}
	}

	private static void ValidateRegionStorage(Region? storageRegion, Guid regionId)
	{
		if (storageRegion is null)
		{
			throw new NotFoundRegionException(regionId);
		}
	}
}