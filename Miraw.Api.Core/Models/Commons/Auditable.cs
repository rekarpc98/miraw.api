namespace Miraw.Api.Core.Models.Commons
{
	public class Auditable
	{
		public DateTimeOffset CreatedDate { get; set; }
		public DateTimeOffset UpdatedDate { get; set; }
		public DateTimeOffset? DeletedDate { get; set; }
		public string CreatedBy { get; set; } = null!;
		public string UpdatedBy { get; set; } = null!;
		public string? DeletedBy { get; set; }
	}
}