using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.Regions.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Regions;

public partial class RegionService
{
	private delegate ValueTask<Region> ReturningRegionFunction();

	private async ValueTask<Region> TryCatch(ReturningRegionFunction returningRegionFunction)
	{
		try
		{
			return await returningRegionFunction();
		}
		catch (InvalidRegionException invalidRegionException)
		{
			throw CreateAndLogValidationException(invalidRegionException);
		}
		catch (NullRegionException nullRegionException)
		{
			throw CreateAndLogValidationException(nullRegionException);
		}
		catch (NotFoundRegionException notFoundRegionException)
		{
			throw CreateAndLogValidationException(notFoundRegionException);
		}
	}

	private RegionValidationException CreateAndLogValidationException(Exception exception)
	{
		var regionValidationException = new RegionValidationException(exception);
		
		_loggingBroker.LogError(regionValidationException);
		
		return regionValidationException;
	}
}