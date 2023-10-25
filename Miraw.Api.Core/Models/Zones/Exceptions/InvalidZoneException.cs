using Xeptions;

namespace Miraw.Api.Core.Models.Zones.Exceptions;

public class InvalidZoneException : Xeption
{
	public InvalidZoneException() : base(message: "Invalid zone. Please fix the errors and try again.")
	{
	}

	public InvalidZoneException(string parameterName, object parameterValue) : base(
		message: $"Invalid zone, parameter name: {parameterName}, parameter value: {parameterValue}.")
	{
	}
}