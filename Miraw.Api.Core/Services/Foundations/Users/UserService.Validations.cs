using Miraw.Api.Core.Models.Users;
using Miraw.Api.Core.Models.Users.Exceptions;

namespace Miraw.Api.Core.Services.Foundations.Users;

public partial class UserService
{
    static void ValidateUserOnRegister(User user)
	{
		ValidateUser(user);

		Validate((IsInvalidX(user.Id), nameof(user.Id)),
			(IsInvalidX(user.Name), nameof(user.Name)),
			(IsInvalidX(user.CreatedDate), nameof(user.CreatedDate)),
			(IsInvalidX(user.UpdatedDate), nameof(user.UpdatedDate)),
			(IsInvalidX(user.Email), nameof(user.Email)),
			(IsInvalidEmail(user.Email), nameof(user.Email)),
			(IsInvalidX(user.RegionId), nameof(user.RegionId)),
			(IfNotNullIsInvalidLength(user.PhoneNumber, 13), nameof(user.PhoneNumber)),
			(IfNotNullIsInvalidPhoneNumber(user.PhoneNumber), nameof(user.PhoneNumber)),
			(IfNotNullIsPhoneNumberContainNonDigit(user.PhoneNumber), nameof(user.PhoneNumber))
		);
	}

	static ValidationRule IsInvalidX(Guid id) => new() { Condition = id == Guid.Empty, Message = "Invalid X Id" };

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

	static ValidationRule IsInvalidX(string text) =>
		new() { Condition = string.IsNullOrWhiteSpace(text), Message = "Text is required" };

	static ValidationRule IsInvalidEmail(string emailText) =>
		new() { Condition = !emailText.Contains('@'), Message = "Email is invalid" };

	static ValidationRule IsInvalidX(DateTimeOffset date) =>
		new() { Condition = date == default, Message = "Date is required" };

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
}

public class ValidationRule
{
	public bool Condition { get; set; }
	public string Message { get; set; } = default!;
}