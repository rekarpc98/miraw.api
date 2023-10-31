using Miraw.Api.Core.Models.Commons;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Models.Regions;

namespace Miraw.Api.Core.Models.Users;

public class User : Auditable
{
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public string? Email { get; set; }
	public string PhoneNumber { get; set; } = null!;
	public Guid RegionId { get; set; }
	public Region? Region { get; set; }
	public Gender Gender { get; set; }

	public Guid PasswordId { get; set; }
	public Password? Password { get; set; }
}