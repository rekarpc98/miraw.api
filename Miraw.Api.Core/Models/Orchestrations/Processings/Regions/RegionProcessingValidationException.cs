using Xeptions;

namespace Miraw.Api.Core.Models.Orchestrations.Processings.Regions;

public class RegionProcessingValidationException : Xeption
{
	public RegionProcessingValidationException(Exception innerException) : base("Region service error occured.",
		innerException)
	{
	}
}