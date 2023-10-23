using Microsoft.EntityFrameworkCore;
using Miraw.Api.Core.Models.Zones;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Brokers.Storages;

public partial class StorageBroker
{
	DbSet<Zone> Zones { get; set; }

	public async ValueTask<Zone> InsertZoneAsync(Zone zone) => await InsertAsync(zone);

	public IQueryable<Zone> SelectAllZones() => SelectAll<Zone>();

	public async ValueTask<Zone?> SelectZoneByIdAsync(Guid zoneId) => await SelectAsync<Zone>(zoneId);

	public async ValueTask<Zone> UpdateZoneAsync(Zone zone) => await UpdateAsync(zone);

	public async ValueTask<Zone> DeleteZoneAsync(Zone zone) => await DeleteAsync(zone);

	public async ValueTask<Zone?> SelectZoneByCoordinateAsync(Point coordinate) =>
		await Zones
			.Where(zone => zone.Boundary.Contains(coordinate))
			.FirstOrDefaultAsync();
}