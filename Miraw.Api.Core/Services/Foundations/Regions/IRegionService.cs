
using System.Drawing;
using Region = Miraw.Api.Core.Models.Regions.Region;

namespace Miraw.Api.Core.Services.Foundations.Regions;

public interface IRegionService
{
	ValueTask<Region> CreateRegionAsync(Region region);
	ValueTask<Region> GetRegionAsync(Guid regionId);
	ValueTask<Region> GetRegionAsync(PointF coordinates);
	ValueTask<IQueryable<Region>> RetrieveAllRegionsAsync();
	ValueTask<Region> EditRegionAsync(Region region);
	ValueTask<Region> RemoveRegionAsync(Region region);
}