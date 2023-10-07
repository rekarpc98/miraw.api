
using Microsoft.EntityFrameworkCore;
using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Brokers.Storages;

public partial class StorageBroker
{
    DbSet<User> Users { get; set; }
    
    public async ValueTask<User> InsertUserAsync(User user) => await InsertAsync(user);

    public IQueryable<User> SelectAllUsers() => SelectAll<User>();

    public async ValueTask<User?> SelectUserByIdAsync(Guid userId) => await SelectAsync<User>(userId);

    public async ValueTask<User> UpdateUserAsync(User user) => await UpdateAsync(user);

    public async ValueTask<User> DeleteUserAsync(User user) => await DeleteAsync(user);
}