using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Models.Zones.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Zones;

public partial class ZoneService
{
	private delegate ValueTask<Zone> ReturningZoneFunction();
	
	private ZoneValidationException CreateZoneValidationExceptionAndLogError(Exception exception)
	{
		var zoneValidationException = new ZoneValidationException(exception);
		loggingBroker.LogError(zoneValidationException);

		return zoneValidationException;
	}

	private async ValueTask<Zone> TryCatch(ReturningZoneFunction returningZoneFunction)
	{
		try
		{
			return await returningZoneFunction();
		}
		catch (NullZoneException nullZoneException)
		{
			throw CreateZoneValidationExceptionAndLogError(nullZoneException);
		}
		catch (InvalidZoneException invalidZoneException)
		{
			throw CreateZoneValidationExceptionAndLogError(invalidZoneException);
		}
		catch (NotFoundZoneException notFoundZoneException)
		{
			throw CreateZoneValidationExceptionAndLogError(notFoundZoneException);
		}
	}
}