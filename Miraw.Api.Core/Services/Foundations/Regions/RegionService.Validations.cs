using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Regions.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Regions;

public partial class RegionService
{
	static void ValidateRegionOnCreate(Region? region)
	{
		ValidateRegion(region);
	}

	static void ValidateRegion(Region? region)
	{
		if (region is null)
		{
			throw new NullRegionException();
		}
	}

	static void ValidateRegionId(Guid regionId)
	{
		if (regionId == Guid.Empty)
		{
			throw new InvalidRegionException(parameterName: nameof(Region.Id), parameterValue: regionId);
		}
	}

	static void ValidateRegionStorage(Region? storageRegion, Guid regionId)
	{
		if (storageRegion is null)
		{
			throw new NotFoundRegionException(regionId);
		}
	}
}