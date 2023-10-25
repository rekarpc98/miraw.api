using System.Diagnostics.Eventing.Reader;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Zones;
using Miraw.Api.Core.Models.Zones.Exceptions;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Services.Foundations.Zones;

public partial class ZoneService : IZoneService
{
	private readonly IStorageBroker storageBroker;
	private readonly ILoggingBroker loggingBroker;

	public ZoneService(IStorageBroker storageBroker, ILoggingBroker loggingBroker)
	{
		this.storageBroker = storageBroker;
		this.loggingBroker = loggingBroker;
	}

	public async ValueTask<Zone> CreateZoneAsync(Zone zone)
	{
		try
		{
			ValidateZoneOnCreate(zone);
			return await storageBroker.InsertZoneAsync(zone);
		}
		catch (NullZoneException nullZoneException)
		{
			throw CreateZoneValidationExceptionAndLogError(nullZoneException);
		}
		catch (InvalidZoneException invalidZoneException)
		{
			throw CreateZoneValidationExceptionAndLogError(invalidZoneException);
		}
	}

	public async ValueTask<Zone> RetrieveZoneByIdAsync(Guid zoneId)
	{
		try
		{
			ValidateZoneId(zoneId);
			Zone? maybeZone = await storageBroker.SelectZoneByIdAsync(zoneId);
			ValidateStorageZone(maybeZone, zoneId);
			
			return maybeZone!;
		}
		catch (NullZoneException nullZoneException)
		{
			throw CreateZoneValidationExceptionAndLogError(nullZoneException);
		}
		catch (InvalidZoneException invalidZoneException)
		{
			throw CreateZoneValidationExceptionAndLogError(invalidZoneException);
		}
		catch (NotFoundZoneException notFoundZoneException)
		{
			throw CreateZoneValidationExceptionAndLogError(notFoundZoneException);
		}
	}

	public async ValueTask<Zone> RetrieveZoneByCoordinateAsync(Point coordinate) =>
		await storageBroker.SelectZoneByCoordinateAsync(coordinate);

	public async ValueTask<IQueryable<Zone>> RetrieveAllZonesAsync() => storageBroker.SelectAllZones();

	public async ValueTask<Zone> ModifyZoneAsync(Zone zone) => await storageBroker.UpdateZoneAsync(zone);

	public async ValueTask<Zone> RemoveZoneAsync(Zone zone) => 
		await storageBroker.DeleteZoneAsync(zone);
}