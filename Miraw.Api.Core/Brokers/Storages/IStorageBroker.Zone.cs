using Miraw.Api.Core.Models.Zones;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask<Zone> InsertZoneAsync(Zone zone);
    IQueryable<Zone> SelectAllZones();
    ValueTask<Zone?> SelectZoneByIdAsync(Guid zoneId);
    ValueTask<Zone> UpdateZoneAsync(Zone zone);
    ValueTask<Zone> DeleteZoneAsync(Zone zone);
    ValueTask<Zone?> SelectZoneByCoordinateAsync(Point coordinate);
}