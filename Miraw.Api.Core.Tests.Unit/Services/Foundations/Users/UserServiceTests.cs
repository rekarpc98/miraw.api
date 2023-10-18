﻿using System.Linq.Expressions;
using Miraw.Api.Core.Brokers.DateTimes;
using Miraw.Api.Core.Brokers.Loggings;
using Miraw.Api.Core.Brokers.Storages;
using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Models.Users.Exceptions;
using Miraw.Api.Core.Services.Foundations.Users;
using Moq;
using Tynamix.ObjectFiller;
using Xeptions;

namespace Miraw.Api.Core.Tests.Unit.Services.Foundations.Users;

public partial class UserServiceTests
{
	readonly Mock<IStorageBroker> _storageBrokerMock = new();
	readonly Mock<IDateTimeBroker> _dateTimeBrokerMock = new();
	readonly Mock<ILoggingBroker> _loggingBrokerMock = new();
	readonly IUserService _userService;

	public UserServiceTests()
	{
		_userService = new UserService(
			storageBroker: _storageBrokerMock.Object,
			dateTimeBroker: _dateTimeBrokerMock.Object,
			loggingBroker: _loggingBrokerMock.Object);
	}

	static DateTimeOffset GetRandomDateTime() => new DateTimeRange(earliestDate: new DateTime()).GetValue();

	static User CreateRandomUser() => CreateUserFiller(DateTimeOffset.UtcNow).Create();
	static User CreateRandomUser(DateTimeOffset date) => CreateUserFiller(date).Create();
	static User CreateRandomUser(DateTimeOffset date, string name) => CreateUserFiller(date, name).Create();

	static IQueryable<User> CreateRandomUsers(DateTimeOffset date)
	{
		var users = new List<User>();
		for (int i = 0; i < GetRandomNumber(); i++)
		{
			users.Add(CreateUserFiller(date).Create());
		}

		return users.AsQueryable();
	}

	static Filler<User> CreateUserFiller(DateTimeOffset date)
	{
		var filler = new Filler<User>();
		Guid userId = Guid.NewGuid();
		Guid regionId = Guid.NewGuid();

		var randomNumber = Random.Shared.Next(0, 1);
		var randomGender = (Gender)randomNumber;

		filler.Setup()
			.OnProperty(x => x.CreatedDate)
			.Use(date)
			.OnProperty(x => x.UpdatedDate)
			.Use(date)
			.OnProperty(x => x.DeletedDate)
			.IgnoreIt()
			.OnProperty(x => x.DeletedBy)
			.IgnoreIt()
			.OnProperty(x => x.CreatedBy)
			.Use(userId)
			.OnProperty(x => x.UpdatedBy)
			.Use(userId)
			.OnProperty(x => x.Name)
			.Use(new RealNames(NameStyle.FirstName))
			.OnProperty(x => x.Email)
			.Use(new EmailAddresses())
			.OnProperty(x => x.PhoneNumber)
			.Use(new PatternGenerator("{N:13}"))
			.OnProperty(x => x.RegionId)
			.Use(regionId)
			.OnProperty(x => x.Gender)
			.Use(randomGender)
			.OnProperty(x => x.Region)
			.IgnoreIt();

		return filler;
	}

	static Filler<User> CreateUserFiller(DateTimeOffset date, string name)
	{
		var filler = new Filler<User>();
		Guid userId = Guid.NewGuid();
		Guid regionId = Guid.NewGuid();

		var randomNumber = Random.Shared.Next(0, 1);
		var randomGender = (Gender)randomNumber;

		filler.Setup()
			.OnProperty(x => x.CreatedDate)
			.Use(date)
			.OnProperty(x => x.UpdatedDate)
			.Use(date)
			.OnProperty(x => x.DeletedDate)
			.IgnoreIt()
			.OnProperty(x => x.DeletedBy)
			.IgnoreIt()
			.OnProperty(x => x.CreatedBy)
			.Use(userId)
			.OnProperty(x => x.UpdatedBy)
			.Use(userId)
			.OnProperty(x => x.Name)
			.Use(name)
			.OnProperty(x => x.Email)
			.Use(new EmailAddresses())
			.OnProperty(x => x.PhoneNumber)
			.Use(new PatternGenerator("{N:13}"))
			.OnProperty(x => x.RegionId)
			.Use(regionId)
			.OnProperty(x => x.Gender)
			.Use(randomGender)
			.OnProperty(x => x.Region)
			.IgnoreIt();

		return filler;
	}

	static int GetRandomNumber() => new IntRange(min: 2, max: 90).GetValue();

	static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedUserValidationException)
	{
		return actualException => actualException.SameExceptionAs(expectedUserValidationException);
	}
}