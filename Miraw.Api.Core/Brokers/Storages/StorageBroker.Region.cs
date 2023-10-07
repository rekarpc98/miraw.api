using Microsoft.EntityFrameworkCore;
using Miraw.Api.Core.Models.Regions;

namespace Miraw.Api.Core.Brokers.Storages;

public partial class StorageBroker
{
	DbSet<Region> Regions { get; set; }

	public async ValueTask<Region> InsertRegionAsync(Region region) => await InsertAsync(region);

	public IQueryable<Region> SelectAllRegions() => SelectAll<Region>();

	public async ValueTask<Region?> SelectRegionByIdAsync(Guid regionId) => await SelectAsync<Region>(regionId);

	public async ValueTask<Region> UpdateRegionAsync(Region region) => await UpdateAsync(region);

	public async ValueTask<Region> DeleteRegionAsync(Region region) => await DeleteAsync(region);
}