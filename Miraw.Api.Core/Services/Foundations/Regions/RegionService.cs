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
	public async ValueTask<Region> CreateRegionAsync(Region region)
	{
		try
		{
			ValidateRegionOnCreate(region);
			
			return await _storageBroker.InsertRegionAsync(region);
		}
		catch (NullRegionException nullRegionException)
		{
			var regionValidationException = new RegionValidationException(nullRegionException);
			
			_loggingBroker.LogError(regionValidationException);
			
			throw regionValidationException;
		}
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