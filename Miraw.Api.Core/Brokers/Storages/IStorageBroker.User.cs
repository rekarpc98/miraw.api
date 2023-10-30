using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Brokers.Storages;

public partial interface IStorageBroker
{
    ValueTask<User> InsertUserAsync(User user);
    IQueryable<User> SelectAllUsers();
    ValueTask<User?> SelectUserByIdAsync(Guid userId);
    ValueTask<User?> SelectUsersByPhoneNumber(string phoneNumber);
    IQueryable<User> SelectUsersByName(string userName);
    ValueTask<User> UpdateUserAsync(User user);
    ValueTask<User> DeleteUserAsync(User user);
}