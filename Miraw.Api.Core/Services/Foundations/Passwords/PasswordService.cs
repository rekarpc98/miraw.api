using System.Net.Sockets;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Passwords;

namespace Miraw.Api.Core.Services.Foundations.Passwords;

public class PasswordService : IPasswordService
{
	private readonly IStorageBroker storageBroker;

	public PasswordService(IStorageBroker storageBroker)
	{
		this.storageBroker = storageBroker;
	}
	
	public async ValueTask<Password> CreatePasswordAsync(Password password) => 
		await storageBroker.InsertPasswordAsync(password);

	public async ValueTask<Password> UpdatePasswordAsync(Password password)
	{
		throw new NotImplementedException();
	}

	public async ValueTask<Password> RetrievePasswordByUserIdAsync(Guid userId)
	{
		throw new NotImplementedException();
	}
}