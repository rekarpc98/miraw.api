﻿using System.Text.Json.Serialization;
using Miraw.Api.Core.Models.Commons;
using Miraw.Api.Core.Models.Operators;
using Miraw.Api.Core.Models.Regions;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Models.Zones;

public class Zone : Auditable
{
	public Guid Id { get; set; }
	public Guid RegionId { get; set; }
	public Region? Region { get; set; }
	public Geometry Boundary { get; set; } = null!;
	
	[JsonIgnore]
	public IEnumerable<Operator> Operators { get; set; } = null!;
}