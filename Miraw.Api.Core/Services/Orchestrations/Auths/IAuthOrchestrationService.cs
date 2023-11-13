namespace Miraw.Api.Core.Services.Orchestrations.Auths;

public interface IAuthOrchestrationService
{
	ValueTask<string> CreateAuthTokenForUserAsync(Guid userId);
	ValueTask<string> LoginUserAsync(string phoneNumber, string password);
	ValueTask<Guid> ValidateAuthTokenAsync(string authToken);
	ValueTask LogoutUserAsync(Guid userId);
}