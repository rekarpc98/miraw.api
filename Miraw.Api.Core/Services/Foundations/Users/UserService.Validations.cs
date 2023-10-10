using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Models.Users.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Users;

public partial class UserService
{
	static void ValidateUserOnRegister(User user)
	{
		ValidateUser(user);

		Validate((IsInvalid(user.Id), nameof(user.Id)),
			(IsInvalid(user.Name), nameof(user.Name)),
			(IsInvalid(user.CreatedDate), nameof(user.CreatedDate)),
			(IsInvalid(user.UpdatedDate), nameof(user.UpdatedDate)),
			(IsInvalid(user.Email), nameof(user.Email)),
			(IsInvalidEmail(user.Email), nameof(user.Email)),
			(IsInvalid(user.RegionId), nameof(user.RegionId)),
			(IfNotNullIsInvalidLength(user.PhoneNumber, 13), nameof(user.PhoneNumber)),
			(IfNotNullIsInvalidPhoneNumber(user.PhoneNumber), nameof(user.PhoneNumber)),
			(IfNotNullIsPhoneNumberContainNonDigit(user.PhoneNumber), nameof(user.PhoneNumber)),
			(IsNotSame(user.UpdatedDate, user.CreatedDate, nameof(User.CreatedDate)), nameof(user.UpdatedDate))
		);
	}

	void ValidateUserOnModify(User user)
	{
		Validate((IsInvalid(user.Id), nameof(user.Id)),
			(IsInvalid(user.Name), nameof(user.Name)),
			(IsInvalid(user.CreatedDate), nameof(user.CreatedDate)),
			(IsInvalid(user.UpdatedDate), nameof(user.UpdatedDate)),
			(IsInvalid(user.Email), nameof(user.Email)),
			(IsInvalidEmail(user.Email), nameof(user.Email)),
			(IsInvalid(user.RegionId), nameof(user.RegionId)),
			(IfNotNullIsInvalidLength(user.PhoneNumber, 13), nameof(user.PhoneNumber)),
			(IfNotNullIsInvalidPhoneNumber(user.PhoneNumber), nameof(user.PhoneNumber)),
			(IfNotNullIsPhoneNumberContainNonDigit(user.PhoneNumber), nameof(user.PhoneNumber)),
			(IsNotRecent(user.UpdatedDate), nameof(user.UpdatedDate)),
			(IsSame(user.UpdatedDate, user.CreatedDate, nameof(user.CreatedDate)), nameof(user.UpdatedDate))
		);
	}

	static ValidationRule IsInvalid(Guid id) => new() { Condition = id == Guid.Empty, Message = "Invalid X Id" };

	static ValidationRule IfNotNullIsInvalidLength(string? text, int length)
	{
		if (text is null)
		{
			return new ValidationRule();
		}

		return new ValidationRule { Condition = text.Length != length, Message = "Text is invalid" };
	}

	static ValidationRule IfNotNullIsInvalidPhoneNumber(string? phoneNumber)
	{
		if (phoneNumber is null)
		{
			return new ValidationRule();
		}

		return new ValidationRule { Condition = phoneNumber.Length != 13, Message = "Phone number is invalid" };
	}

	static ValidationRule IfNotNullIsPhoneNumberContainNonDigit(string? phoneNumber)
	{
		if (phoneNumber is null)
		{
			return new ValidationRule();
		}

		return new ValidationRule
		{
			Condition = phoneNumber.Any(character => !char.IsDigit(character)),
			Message = "Phone number must contain only digits"
		};
	}

	static ValidationRule IsInvalid(string text) =>
		new() { Condition = string.IsNullOrWhiteSpace(text), Message = "Text is required" };

	static ValidationRule IsInvalidEmail(string emailText) =>
		new() { Condition = !emailText.Contains('@'), Message = "Email is invalid" };

	static ValidationRule IsInvalid(DateTimeOffset date) =>
		new() { Condition = date == default, Message = "Date is required" };

	ValidationRule IsNotRecent(DateTimeOffset date) =>
		new() { Condition = IsDateNotRecent(date), Message = "Date is not recent" };

	static ValidationRule IsSame(DateTimeOffset firstDate, DateTimeOffset secondDate, string secondDateName) =>
		new ValidationRule { Condition = firstDate == secondDate, Message = $"Date is the same as {secondDateName}" };

	static ValidationRule IsNotSame(DateTimeOffset firstDate, DateTimeOffset secondDate, string secondDateName) =>
		new ValidationRule { Condition = firstDate != secondDate, Message = $"Date is not the same as {secondDateName}" };

	static ValidationRule IsNotSame(Guid firstId, Guid secondId, string secondIdName) =>
		new() { Condition = firstId != secondId, Message = $"Id is not the same as {secondIdName}" };


	bool IsDateNotRecent(DateTimeOffset date)
	{
		var currentDate = _dateTimeBroker.GetCurrentDateTime();

		var timeDifference = currentDate - date;

		return timeDifference.Duration() > TimeSpan.FromMinutes(1);
	}

	static void ValidateUser(User user)
	{
		if (user is null)
		{
			throw new NullUserException();
		}
	}

	static void Validate(params (ValidationRule Rule, string Parameter)[] validations)
	{
		var invalidUserException = new InvalidUserException();

		foreach ((ValidationRule rule, string parameter) in validations)
		{
			if (rule.Condition)
			{
				invalidUserException.UpsertDataList(key: parameter, value: rule.Message);
			}
		}

		invalidUserException.ThrowIfContainsErrors();
	}

	static void ValidateStorageUser(User? storageUser, Guid userId)
	{
		if (storageUser is null)
		{
			throw new NotFoundUserException(userId);
		}
	}
	
	static void ValidateStorageUser(User? storageUser, string userName)
	{
		if (storageUser is null)
		{
			throw new NotFoundUserException(userName);
		}
	}

	static void ValidateAgainstStorageUserOnModify(User inputUser, User storageUser)
	{
		Validate(
			(IsNotSame(inputUser.CreatedDate, storageUser.CreatedDate, nameof(User.CreatedDate)),
				nameof(User.CreatedDate)),
			(IsSame(inputUser.UpdatedDate, storageUser.UpdatedDate, nameof(User.UpdatedDate)),
				nameof(User.UpdatedDate)),
			(IsNotSame(inputUser.CreatedBy, storageUser.CreatedBy, nameof(storageUser.CreatedBy)),
				nameof(storageUser.CreatedBy))
		);
	}

	static void ValidateUserName(string userName)
	{ 
		Validate((IsInvalid(userName), nameof(userName)));
	}
}

public class ValidationRule
{
	public bool Condition { get; set; }
	public string Message { get; set; } = default!;
}