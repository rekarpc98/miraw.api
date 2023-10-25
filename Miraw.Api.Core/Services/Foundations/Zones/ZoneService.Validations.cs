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
			(IsInvalid(zone.Id), nameof(Zone.Id)),
			(IsInvalid(zone.RegionId), nameof(Zone.RegionId)),
			(IsInvalid(zone.Boundary), nameof(Zone.Boundary)),
			(IsInvalid(zone.CreatedDate), nameof(Zone.CreatedDate)),
			(IsInvalid(zone.UpdatedDate), nameof(Zone.UpdatedDate)),
			(IsInvalid(zone.CreatedBy), nameof(Zone.CreatedBy)),
			(IsInvalid(zone.UpdatedBy), nameof(Zone.UpdatedBy)),
			(IsNotSame(zone.UpdatedDate, zone.CreatedDate, nameof(Zone.CreatedDate)), nameof(Zone.UpdatedDate))
		);
	}

	private static dynamic IsNotSame(DateTimeOffset firstDate, DateTimeOffset secondDate, string secondDateName)
	{
		return new
		{
			Condition = firstDate != secondDate,
			Message = $"Date is not the same as {secondDateName}"
		};
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
				invalidZoneException.UpsertDataList(parameter, rule.Message);
			}
		}
		
		invalidZoneException.ThrowIfContainsErrors();
	}

	private static void ValidateZoneId(Guid zoneId)
	{
		if (zoneId == Guid.Empty)
		{
			throw new InvalidZoneException(nameof(Zone.Id), zoneId);
		}
	}

	private static void ValidateStorageZone(Zone? maybeZone, Guid zoneId)
	{
		if (maybeZone is null)
		{
			throw new NotFoundZoneException(zoneId);
		}
	}

	private static void ValidateStorageZone(Zone? maybeZone, Point coordinate)
	{
		if (maybeZone is null)
		{
			throw new NotFoundZoneException(coordinate);
		}
	}

	private static void ValidateCoordinate(Point? coordinate)
	{
		if (coordinate is null)
		{
			throw new InvalidZoneException("Coordinate", coordinate!);
		}

		if (coordinate.IsEmpty)
		{
			throw new InvalidZoneException("Coordinate", coordinate);
		}

		if (coordinate is { X: 0, Y: 0 })
		{
			throw new InvalidZoneException("Coordinate", coordinate);
		}
	}
}