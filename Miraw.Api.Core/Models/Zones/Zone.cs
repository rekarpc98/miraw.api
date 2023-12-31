﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Miraw.Api.Core.Models.Commons;
using Miraw.Api.Core.Models.Regions;
using Miraw.Api.Core.Models.ZoneOperators;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Models.Zones;

public class Zone : Auditable
{
	public Guid Id { get; set; }
	public Guid RegionId { get; set; }
	public Region? Region { get; set; }
	
	[Column (TypeName = "geometry")]
	public Geometry Boundary { get; set; } = null!;
	
	[JsonIgnore]
	public IEnumerable<ZoneOperator> ZoneOperators { get; set; } = null!;
}