using Miraw.Api.Core.Models.Zones.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Zones;

public partial class ZoneService
{
	private Exception CreateZoneValidationExceptionAndLogError(Exception nullZoneException)
	{
		var zoneValidationException = new ZoneValidationException(nullZoneException);

		loggingBroker.LogError(zoneValidationException);

		return zoneValidationException;
	}
}