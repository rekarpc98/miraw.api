using Miraw.Api.Core.Models.Zones;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Services.Foundations.Zones;

public interface IZoneService
{
	ValueTask<Zone> CreateZoneAsync(Zone zone);
	ValueTask<Zone> RetrieveZoneByIdAsync(Guid zoneId);
	ValueTask<Zone> RetrieveZoneByCoordinateAsync(Point coordinate);
	ValueTask<IQueryable<Zone>> RetrieveAllZonesAsync();
	ValueTask<Zone> ModifyZoneAsync(Zone zone);
	ValueTask<Zone> RemoveZoneAsync(Zone zone);
}