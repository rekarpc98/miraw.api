using Miraw.Api.Core.Models.Passwords.Exceptions;
using Miraw.Api.Core.Services.Foundations.Users;

namespace Miraw.Api.Core.Services.Processings.Passwords;

public partial class PasswordProcessingService
{
	private static void ValidateString(string passwordString)
	{
		Validate((ValidateLength(passwordString), "Length"));
	}
	
	private static ValidationRule ValidateLength(string text)
	{
		const int minAllowedLength = 5;
		return new ValidationRule
		{
			Condition = text.Length < minAllowedLength,
			Message = $"Password should be at least {minAllowedLength} characters long."
		};
	}
	
	private static void Validate(params (ValidationRule, string Parameter)[] rules)
	{
		var invalidPasswordException = new InvalidPasswordException();

		foreach ((ValidationRule rule, string parameter) in rules)
		{
			if (rule.Condition)
			{
				invalidPasswordException.UpsertDataList(
					key: parameter, 
					value: rule.Message);
			}
		}
		
		invalidPasswordException.ThrowIfContainsErrors();
	}
}