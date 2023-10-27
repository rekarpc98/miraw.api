using Xeptions;

namespace Miraw.Api.Core.Models.Processings.Regions;

public class RegionProcessingValidationException : Xeption
{
	public RegionProcessingValidationException(Exception innerException) : base("Region service error occured.",
		innerException)
	{
	}
}