using System.Drawing;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Regions.Exceptions;
using Region = Miraw.Api.Core.Models.Regions.Region;

namespace Miraw.Api.Core.Services.Foundations.Regions;

public partial class RegionService : IRegionService
{
	readonly IStorageBroker _storageBroker;
	readonly ILoggingBroker _loggingBroker;

	public RegionService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
	{
		_storageBroker = storageBroker;
		_loggingBroker = loggingBroker;
	}

	public ValueTask<Region> CreateRegionAsync(Region region) =>
		TryCatch(async () =>
		{
			ValidateRegionOnCreate(region);

			return await _storageBroker.InsertRegionAsync(region);
		});

	public ValueTask<Region> GetRegionAsync(Guid regionId) =>
		TryCatch(async () =>
		{
			ValidateRegionId(regionId);

			Region? storageRegion = await _storageBroker.SelectRegionByIdAsync(regionId);

			ValidateRegionStorage(storageRegion, regionId);

			return storageRegion!;
		});

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