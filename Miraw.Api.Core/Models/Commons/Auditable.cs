namespace Miraw.Api.Core.Models.Commons;

public class Auditable
{
	public DateTimeOffset CreatedDate { get; set; }
	public DateTimeOffset UpdatedDate { get; set; }
	public DateTimeOffset? DeletedDate { get; set; }
	public Guid CreatedBy { get; set; }
	public Guid UpdatedBy { get; set; }
	public Guid? DeletedBy { get; set; }
}