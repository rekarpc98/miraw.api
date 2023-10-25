using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Regions;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Services.Foundations.Regions;

public partial class RegionService : IRegionService
{
	private readonly IStorageBroker storageBroker;
	private readonly ILoggingBroker loggingBroker;

	public RegionService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
	{
		this.storageBroker = storageBroker;
		this.loggingBroker = loggingBroker;
	}

	public ValueTask<Region> CreateRegionAsync(Region region) =>
		TryCatch(async () =>
		{
			ValidateRegionOnCreate(region);

			return await storageBroker.InsertRegionAsync(region);
		});

	public ValueTask<Region> RetrieveRegionAsync(Guid regionId) =>
		TryCatch(async () =>
		{
			ValidateRegionId(regionId);

			Region? storageRegion = await storageBroker.SelectRegionByIdAsync(regionId);

			ValidateRegionStorage(storageRegion, regionId);

			return storageRegion!;
		});

	public async ValueTask<Region> RetrieveRegionAsync(Point coordinates)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<IQueryable<Region>> RetrieveAllRegionsAsync()
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Region> ModifyRegionAsync(Region region)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Region> RemoveRegionAsync(Region region)
	{
		throw new NotImplementedException();
	}
}