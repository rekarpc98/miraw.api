using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Zones;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Services.Foundations.Zones;

public class ZoneService : IZoneService
{
	private readonly IStorageBroker storageBroker;
	private readonly ILoggingBroker loggingBroker;

	public ZoneService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
	{
		this.storageBroker = storageBroker;
		this.loggingBroker = loggingBroker;
	}

	public async ValueTask<Zone> CreateZoneAsync(Zone zone) => await storageBroker.InsertZoneAsync(zone);

	public async ValueTask<Zone> RetrieveZoneByIdAsync(Guid zoneId) => await storageBroker.SelectZoneByIdAsync(zoneId);

	public async ValueTask<Zone> RetrieveZoneByCoordinateAsync(Point coordinate) =>
		await storageBroker.SelectZoneByCoordinateAsync(coordinate);

	public async ValueTask<IQueryable<Zone>> RetrieveAllZonesAsync() => storageBroker.SelectAllZones();

	public async ValueTask<Zone> ModifyZoneAsync(Zone zone) => await storageBroker.UpdateZoneAsync(zone);

	public async ValueTask<Zone> RemoveZoneAsync(Zone zone) => 
		await storageBroker.DeleteZoneAsync(zone);
}