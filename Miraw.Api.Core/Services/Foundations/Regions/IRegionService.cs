﻿

using Miraw.Api.Core.Models.Regions;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Services.Foundations.Regions;

public interface IRegionService
{
	ValueTask<Region> CreateRegionAsync(Region region);
	ValueTask<Region> RetrieveRegionAsync(Guid regionId);
	ValueTask<Region> RetrieveRegionAsync(Point coordinates);
	ValueTask<IQueryable<Region>> RetrieveAllRegionsAsync();
	ValueTask<Region> ModifyRegionAsync(Region region);
	ValueTask<Region> RemoveRegionAsync(Region region);
}