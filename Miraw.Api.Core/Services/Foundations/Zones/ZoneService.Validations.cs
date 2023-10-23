using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Models.Zones.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Zones;

public partial class ZoneService
{
	private static void ValidateZoneOnCreate(Zone? zone)
	{
		ValidateZone(zone);
	}

	private static void ValidateZone(Zone? zone)
	{
		if (zone is null)
		{
			throw new NullZoneException();
		}
	}

}