using Xeptions;

namespace Miraw.Api.Core.Models.Regions.Exceptions;

public class RegionValidationException : Xeption
{
	public RegionValidationException(Exception innerException)
		: base(message: "Invalid input, contact support.", innerException)
	{
	}
}