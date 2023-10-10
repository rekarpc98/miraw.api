using System.ComponentModel.DataAnnotations.Schema;
using Miraw.Api.Core.Models.Commons;
using NetTopologySuite.Geometries;

namespace Miraw.Api.Core.Models.Regions;

public class Region : Auditable
{
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	
	[Column (TypeName = "geometry")]
	public Geometry Boundary { get; set; } = null!;
}