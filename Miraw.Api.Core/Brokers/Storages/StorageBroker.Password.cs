using Microsoft.EntityFrameworkCore;
using Miraw.Api.Core.Models.Passwords;
using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Brokers.Storages;

public partial class StorageBroker
{
	private DbSet<Password> Passwords { get; set; }

	public async ValueTask<Password> InsertPasswordAsync(Password password) => await InsertAsync(password);

	public async ValueTask<Password?> SelectPasswordByUserIdAsync(Guid userId) =>
		await Passwords
			.AsNoTracking()
			.Include(password => password.User)
			.FirstOrDefaultAsync(password => password.UserId == userId);

	public async ValueTask<Password> UpdatePasswordAsync(Password password) => await UpdateAsync(password);

	public async ValueTask<Password> DeletePasswordAsync(Password password) => await DeleteAsync(password);
}