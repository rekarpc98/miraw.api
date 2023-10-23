using Xeptions;

namespace Miraw.Api.Core.Models.Zones.Exceptions;

public class NullZoneException : Xeption
{
	public NullZoneException() : base("Zone is null.")
	{
	}
}