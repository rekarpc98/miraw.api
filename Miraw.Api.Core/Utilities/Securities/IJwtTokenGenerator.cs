using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Utilities.Securities;

public interface IJwtTokenGenerator
{
	string GenerateTokenForUser(User user);
}