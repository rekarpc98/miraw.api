using Miraw.Api.Core.Models.Zones.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Zones;

public partial class ZoneService
{
	private ZoneValidationException CreateZoneValidationExceptionAndLogError(Exception exception)
	{
		var zoneValidationException = new ZoneValidationException(exception);
		loggingBroker.LogError(zoneValidationException);

		return zoneValidationException;
	}
}