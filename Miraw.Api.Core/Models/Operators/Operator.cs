using System.Text.Json.Serialization;
using Miraw.Api.Core.Models.Commons;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Models.ZoneOperators;

namespace Miraw.Api.Core.Models.Operators;

public class Operator : Auditable
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public User? User { get; set; }
	
	[JsonIgnore]
	public IEnumerable<ZoneOperator> ZoneOperators { get; set; } = null!;
}