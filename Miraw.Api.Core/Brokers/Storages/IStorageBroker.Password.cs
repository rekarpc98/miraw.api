using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask<Password> InsertPasswordAsync(Password password);
    ValueTask<Password?> SelectPasswordByUserIdAsync(Guid userId);
    ValueTask<Password> UpdatePasswordAsync(Password password);
    ValueTask<Password> DeletePasswordAsync(Password password);
}