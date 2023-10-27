using Xeptions;

namespace Miraw.Api.Core.Models.Orchestrations.Processings.Regions;

public class RegionProcessingDependencyValidationException : Xeption
{
	public RegionProcessingDependencyValidationException(Exception innerException) : base("Region service error occured.",
		innerException)
	{
	}
	public RegionProcessingDependencyValidationException() : base("Region service error occured.")
	{
	}
}