using Miraw.Api.Core.Models.Regions;

namespace Miraw.Api.Core.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask<Region> InsertRegionAsync(Region region);
    IQueryable<Region> SelectAllRegions();
    ValueTask<Region?> SelectRegionByIdAsync(Guid regionId);
    ValueTask<Region> UpdateRegionAsync(Region region);
    ValueTask<Region> DeleteRegionAsync(Region region);
}