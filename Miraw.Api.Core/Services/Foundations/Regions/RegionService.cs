using System.Drawing;
using Miraw.Api.Core.Brokers.Storages;
using Region = Miraw.Api.Core.Models.Regions.Region;

namespace Miraw.Api.Core.Services.Foundations.Regions;

public partial class RegionService : IRegionService
{
	readonly IStorageBroker _storageBroker;

	public RegionService(IStorageBroker storageBroker)
	{
		_storageBroker = storageBroker;
	}
	public async ValueTask<Region> CreateRegionAsync(Region region)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Region> GetRegionAsync(Guid regionId)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Region> GetRegionAsync(PointF coordinates)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<IQueryable<Region>> RetrieveAllRegionsAsync()
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Region> EditRegionAsync(Region region)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Region> RemoveRegionAsync(Region region)
	{
		throw new NotImplementedException();
	}
}