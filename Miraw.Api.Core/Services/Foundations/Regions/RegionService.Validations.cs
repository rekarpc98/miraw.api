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
}