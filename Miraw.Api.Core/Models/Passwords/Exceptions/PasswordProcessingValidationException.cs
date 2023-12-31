﻿using Xeptions;

namespace Miraw.Api.Core.Models.Passwords.Exceptions;

public class PasswordProcessingValidationException : Xeption
{
	public PasswordProcessingValidationException(Exception innerException)
		: base(message: "Password validation error occurred, please try again.", innerException)
	{
	}
}