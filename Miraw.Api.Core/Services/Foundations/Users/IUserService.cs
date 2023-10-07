﻿using Miraw.Api.Core.Models.Users;

namespace Miraw.Api.Core.Services.Foundations.Users;

public interface IUserService
{
	ValueTask<User> RegisterUserAsync(User user);
	IQueryable<User> RetrieveAllUsers();
	ValueTask<User> RetrieveUserByIdAsync(Guid userId);
	ValueTask<User> ModifyUserAsync(User user);
	ValueTask<User> RemoveUserByIdAsync(Guid userId);
}