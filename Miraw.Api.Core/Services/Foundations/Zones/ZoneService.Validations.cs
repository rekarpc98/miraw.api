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
			(IsInvalid(zone.RegionId), nameof(zone.RegionId)),
			(IsInvalid(zone.Boundary), nameof(zone.Boundary)),
			(IsInvalid(zone.CreatedDate), nameof(zone.CreatedDate)),
			(IsInvalid(zone.UpdatedDate), nameof(zone.UpdatedDate)),
			(IsInvalid(zone.CreatedBy), nameof(zone.CreatedBy)),
			(IsInvalid(zone.UpdatedBy), nameof(zone.UpdatedBy))
		);
	}

	private static dynamic IsInvalid(DateTimeOffset dateTimeOffset) =>
		new
		{
			Condition = dateTimeOffset == default || dateTimeOffset == DateTimeOffset.MinValue,
			Message = "Invalid date time offset"
		};

	private static dynamic IsInvalid(Guid? id) =>
		new { Condition = id == Guid.Empty || id == null, Message = "Invalid guid id" };

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