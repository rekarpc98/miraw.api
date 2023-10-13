
using Xeptions;

namespace Miraw.Api.Core.Models.Regions.Exceptions;

public class NullRegionException : Xeption
{
	public NullRegionException() : base("Region is null.")
	{
		
	}
}