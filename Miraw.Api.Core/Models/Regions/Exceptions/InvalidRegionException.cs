using Xeptions;

namespace Miraw.Api.Core.Models.Regions.Exceptions;

public class InvalidRegionException : Xeption
{
	public InvalidRegionException(string parameterName, object parameterValue)
		: base(
			message: $"Invalid region parameter, parameter name: {parameterName}, parameter value: {parameterValue}.")
	{
	}

	public InvalidRegionException()
		: base(message: "Invalid region. Please try again.")
	{
	}
}