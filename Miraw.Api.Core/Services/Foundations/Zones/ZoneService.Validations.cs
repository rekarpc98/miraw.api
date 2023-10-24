using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Models.Zones.Exceptions;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Services.Foundations.Zones;

public partial class ZoneService
{
	private static void ValidateZoneOnCreate(Zone zone)
	{
		ValidateZone(zone);

		Validate(
			(IsInvalid(zone.Id), nameof(zone.Id)),
			(IsInvalid(zone.Boundary), nameof(zone.Boundary))
		);
	}

	private static dynamic IsInvalid(Guid? zoneId) =>
		new { Condition = zoneId == Guid.Empty || zoneId == null, Message = "Invalid zone id" };

	private static dynamic IsInvalid(Geometry? geometry) =>
		new { Condition = geometry is null || geometry.IsEmpty, Message = "Invalid zone boundary" };

	private static void ValidateZone(Zone? zone)
	{
		if (zone is null)
		{
			throw new NullZoneException();
		}
	}

	private static void Validate(params (dynamic Rule, string Parameter)[] validations)
	{
		var invalidZoneException = new InvalidZoneException();

		foreach ((dynamic rule, string parameter) in validations)
		{
			if (rule.Condition)
			{
				invalidZoneException.AddData(parameter, rule.Message);
			}
		}

		invalidZoneException.ThrowIfContainsErrors();
	}
}