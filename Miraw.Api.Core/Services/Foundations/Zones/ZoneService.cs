﻿using Miraw.Api.Core.Brokers.Storages;
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
	
	public async ValueTask<Zone> CreateZoneAsync(Zone zone) => await storageBroker.InsertZoneAsync(zone);

	public async ValueTask<Zone> RetrieveZoneByIdAsync(Guid zoneId)
	{
		return await storageBroker.SelectZoneByIdAsync(zoneId);
	}

	public async ValueTask<Zone> RetrieveZoneByCoordinateAsync(Point coordinate)
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