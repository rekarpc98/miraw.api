using Xeptions;

namespace Miraw.Api.Core.Models.Regions.Exceptions;

public class NotFoundRegionException : Xeption
{
	public NotFoundRegionException(Guid regionId)
		: base(message: $"Couldn't find region with id: {regionId}.")
	{
	}
}