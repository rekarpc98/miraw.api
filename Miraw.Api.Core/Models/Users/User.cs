using Miraw.Api.Core.Models.Commons;
using Miraw.Api.Core.Models.Regions;

namespace Miraw.Api.Core.Models.Users
{
	public class User : Auditable
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Email { get; set; } = null!;
		public string? PhoneNumber { get; set; }
		public Guid RegionId { get; set; }
		public Region? Region { get; set; }
		public Gender Gender { get; set; }
	}
}