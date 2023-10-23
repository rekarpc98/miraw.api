using Xeptions;

namespace Miraw.Api.Core.Models.Zones.Exceptions;

public class ZoneValidationException : Xeption
{
	public ZoneValidationException() : base("Zone validation error occurred.")
	{
	}
	
	public ZoneValidationException(Exception innerException) : base("Zone validation error occurred.", innerException)
	{
	}
}