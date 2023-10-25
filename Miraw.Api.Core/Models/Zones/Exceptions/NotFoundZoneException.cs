using Xeptions;

namespace Miraw.Api.Core.Models.Zones.Exceptions;

public class NotFoundZoneException : Xeption
{
	public NotFoundZoneException(Guid zoneId)
		: base(message: $"Couldn't find zone with id: {zoneId}.")
	{
	}
}