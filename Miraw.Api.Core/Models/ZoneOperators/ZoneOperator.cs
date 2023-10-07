using Miraw.Api.Core.Models.Operators;
using Miraw.Api.Core.Models.Zones;

namespace Miraw.Api.Core.Models.ZoneOperators;

public class ZoneOperator
{
	public Guid ZoneId { get; set; }
	public Zone? Zone { get; set; }
	public Guid OperatorId { get; set; }
	public Operator? Operator { get; set; }
}