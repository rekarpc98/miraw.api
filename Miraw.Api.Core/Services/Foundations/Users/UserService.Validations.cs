using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Models.Users.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Users;

public partial class UserService
{
	private static void ValidateUserOnRegister(User user)
	{
		ValidateUser(user);

		Validate((IsInvalid(user.Id), nameof(user.Id)),
			(IsInvalid(user.Name), nameof(user.Name)),
			(IsInvalid(user.CreatedDate), nameof(user.CreatedDate)),
			(IsInvalid(user.UpdatedDate), nameof(user.UpdatedDate)),
			(IsInvalidEmail(user.Email), nameof(user.Email)),
			(IsInvalid(user.RegionId), nameof(user.RegionId)),
			(IfNotNullIsInvalidLength(user.PhoneNumber, 13), nameof(user.PhoneNumber)),
			(IfNotNullIsInvalidPhoneNumber(user.PhoneNumber), nameof(user.PhoneNumber)),
			(IfNotNullIsPhoneNumberContainNonDigit(user.PhoneNumber), nameof(user.PhoneNumber)),
			(IsNotSame(user.UpdatedDate, user.CreatedDate, nameof(User.CreatedDate)), nameof(user.UpdatedDate))
		);
	}

	private void ValidateUserOnModify(User user)
	{
		Validate((IsInvalid(user.Id), nameof(user.Id)),
			(IsInvalid(user.Name), nameof(user.Name)),
			(IsInvalid(user.CreatedDate), nameof(user.CreatedDate)),
			(IsInvalid(user.UpdatedDate), nameof(user.UpdatedDate)),
			(IsInvalidEmail(user.Email), nameof(user.Email)),
			(IsInvalid(user.RegionId), nameof(user.RegionId)),
			(IfNotNullIsInvalidLength(user.PhoneNumber, 13), nameof(user.PhoneNumber)),
			(IfNotNullIsInvalidPhoneNumber(user.PhoneNumber), nameof(user.PhoneNumber)),
			(IfNotNullIsPhoneNumberContainNonDigit(user.PhoneNumber), nameof(user.PhoneNumber)),
			(IsNotRecent(user.UpdatedDate), nameof(user.UpdatedDate)),
			(IsSame(user.UpdatedDate, user.CreatedDate, nameof(user.CreatedDate)), nameof(user.UpdatedDate))
		);
	}

	private static ValidationRule IsInvalid(Guid id) =>
		new() { Condition = id == Guid.Empty, Message = "Invalid guid id" };

	private static ValidationRule IfNotNullIsInvalidLength(string? text, int length)
	{
		if (text is null)
		{
			return new ValidationRule();
		}

		return new ValidationRule { Condition = text.Length != length, Message = "Text is invalid" };
	}

	private static ValidationRule IfNotNullIsInvalidPhoneNumber(string? phoneNumber)
	{
		if (phoneNumber is null)
		{
			return new ValidationRule();
		}

		return new ValidationRule { Condition = phoneNumber.Length != 13, Message = "Phone number is invalid" };
	}

	private static ValidationRule IfNotNullIsPhoneNumberContainNonDigit(string? phoneNumber)
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

	private static ValidationRule IsInvalid(string text) =>
		new() { Condition = string.IsNullOrWhiteSpace(text), Message = "Text is required" };

	private static ValidationRule IsInvalidEmail(string? emailText)
	{
		return emailText is null
			? new ValidationRule()
			: new ValidationRule { Condition = !emailText.Contains('@'), Message = "Email is invalid" };
	}

	private static ValidationRule IsInvalid(DateTimeOffset date) =>
		new() { Condition = date == default, Message = "Date is required" };

	private ValidationRule IsNotRecent(DateTimeOffset date) =>
		new() { Condition = IsDateNotRecent(date), Message = "Date is not recent" };

	private static ValidationRule IsSame(DateTimeOffset firstDate, DateTimeOffset secondDate, string secondDateName) =>
		new ValidationRule { Condition = firstDate == secondDate, Message = $"Date is the same as {secondDateName}" };

	private static ValidationRule
		IsNotSame(DateTimeOffset firstDate, DateTimeOffset secondDate, string secondDateName) =>
		new ValidationRule
		{
			Condition = firstDate != secondDate, Message = $"Date is not the same as {secondDateName}"
		};

	private static ValidationRule IsNotSame(Guid firstId, Guid secondId, string secondIdName) =>
		new() { Condition = firstId != secondId, Message = $"Id is not the same as {secondIdName}" };


	private bool IsDateNotRecent(DateTimeOffset date)
	{
		var currentDate = dateTimeBroker.GetCurrentDateTime();

		var timeDifference = currentDate - date;

		return timeDifference.Duration() > TimeSpan.FromMinutes(1);
	}

	private static void ValidateUser(User user)
	{
		if (user is null)
		{
			throw new NullUserException();
		}
	}

	private static void Validate(params (ValidationRule Rule, string Parameter)[] validations)
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

	private static void ValidateStorageUser(User? storageUser, Guid userId)
	{
		if (storageUser is null)
		{
			throw new NotFoundUserException(userId);
		}
	}

	private static void ValidateStorageUser(User? storageUser, string userName)
	{
		if (storageUser is null)
		{
			throw new NotFoundUserException(userName);
		}
	}

	private static void ValidateAgainstStorageUserOnModify(User inputUser, User storageUser)
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

	private static void ValidateUserName(string userName)
	{
		Validate((IsInvalid(userName), nameof(userName)));
	}
}

public class ValidationRule
{
	public bool Condition { get; set; }
	public string Message { get; set; } = default!;
}