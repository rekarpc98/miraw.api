using System.ComponentModel.DataAnnotations.Schema;
using Miraw.Api.Core.Models.Commons;
using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Models.Passwords;

public class Password : Record
{
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public Guid Id { get; set; }
	
	public Guid UserId { get; set; }
	public User? User { get; set; }
	public string PasswordHash { get; set; } = null!;
}