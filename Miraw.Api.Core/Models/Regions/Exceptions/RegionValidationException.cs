using Xeptions;

namespace Miraw.Api.Core.Models.Regions.Exceptions;

public class RegionValidationException : Xeption
{
	public RegionValidationException(string parameterName, object parameterValue)
		: base(message: $"Invalid region, " +
		                $"parameter name: {parameterName}, " +
		                $"parameter value: {parameterValue}.")
	{
	}

	public RegionValidationException(Exception innerException)
		: base(message: "Invalid input, contact support.", innerException)
	{
	}
}