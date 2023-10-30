using Miraw.Api.Core.Models.Commons;

namespace Miraw.Api.Core.Models.Users;

public class Password : Record
{
	public Guid Id { get; set; }
	public Guid UserId { get; set; }
	public User? User { get; set; }
	public string PasswordHash { get; set; } = null!;
}