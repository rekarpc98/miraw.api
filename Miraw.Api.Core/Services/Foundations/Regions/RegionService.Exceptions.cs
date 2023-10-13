﻿using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Regions.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Regions;

public partial class RegionService
{
	delegate ValueTask<Region> ReturningRegionFunction();

	ValueTask<Region> TryCatch(ReturningRegionFunction returningRegionFunction)
	{
		try
		{
			return returningRegionFunction();
		}
		catch (NullRegionException nullRegionException)
		{
			var regionValidationException = new RegionValidationException(nullRegionException);
			
			_loggingBroker.LogError(regionValidationException);
			
			throw regionValidationException;
		}
	}
}