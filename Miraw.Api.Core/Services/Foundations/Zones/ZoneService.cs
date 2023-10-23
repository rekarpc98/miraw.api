using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Zones;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Services.Foundations.Zones;

public class ZoneService : IZoneService
{
	readonly IStorageBroker storageBroker;

	public ZoneService(IStorageBroker storageBroker)
	{
		this.storageBroker = storageBroker;
	}
	
	public async ValueTask<Zone> CreateZoneAsync(Zone zone)
	{
		await storageBroker.InsertZoneAsync(zone);
		throw new NotImplementedException();
	}

	public async ValueTask<Zone> RetrieveZoneAsync(Guid zoneId)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Zone> RetrieveZoneAsync(Point coordinates)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<IQueryable<Zone>> RetrieveAllZonesAsync()
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Zone> ModifyZoneAsync(Zone zone)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Zone> RemoveZoneAsync(Zone zone)
	{
		throw new NotImplementedException();
	}
}