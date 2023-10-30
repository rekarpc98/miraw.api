using Microsoft.EntityFrameworkCore;
using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Brokers.Storages;

public partial class StorageBroker
{
	private DbSet<User> Users { get; set; }

	public async ValueTask<User> InsertUserAsync(User user) => await InsertAsync(user);

	public IQueryable<User> SelectAllUsers() => SelectAll<User>();

	public async ValueTask<User?> SelectUserByIdAsync(Guid userId) => await SelectAsync<User>(userId);

	public async ValueTask<User?> SelectUsersByPhoneNumber(string phoneNumber)
	{
		phoneNumber = phoneNumber.Trim();
		return await Users.FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
	}
	
	public IQueryable<User> SelectUsersByName(string userName)
	{
		userName = userName.Trim();
		return Users.Where(user => SoundsLike(user.Name) == SoundsLike(userName));
	}

	public async ValueTask<User> UpdateUserAsync(User user) => await UpdateAsync(user);

	public async ValueTask<User> DeleteUserAsync(User user) => await DeleteAsync(user);

	[DbFunction(Name = "SoundEx", IsBuiltIn = true)]
	public static string SoundsLike(string keyword) => throw new NotImplementedException();
}