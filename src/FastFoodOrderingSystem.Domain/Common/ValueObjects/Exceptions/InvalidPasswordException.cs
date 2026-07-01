using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

public sealed class InvalidPasswordException : DomainException
{
    private InvalidPasswordException(string code, string message) : base(code, message)
    {
    }

    public static InvalidPasswordException Empty()
    {
        return new InvalidPasswordException(
            "invalid_password_exception.empty",
            "Password must not be empty.");
    }

    public static InvalidPasswordException PasswordLengthBelowMinimum(int minLength)
    {
        return new InvalidPasswordException(
            "inValid_password_exception.password_length_below_minimum",
            $"Password must be at least {minLength} characters.");
    }

    public static InvalidPasswordException PasswordRequiresUppercase()
    {
        return new InvalidPasswordException(
            "invalid_password_exception.password_requires_uppercase",
            "Password must contain at least one uppercase letter.");
    }

    public static InvalidPasswordException PasswordRequiresLowercase()
    {
        return new InvalidPasswordException(
            "invalid_password_exception.password_requires_lowercase",
            "Password must contain at least one lowercase letter.");
    }

    public static InvalidPasswordException PasswordRequiresDigit()
    {
        return new InvalidPasswordException(
            "invalid_password_exception.password_requires_digit",
            "Password must contain at least one digit.");
    }

    public static InvalidPasswordException PasswordRequiresSpecialCharacter()
    {
        return new InvalidPasswordException(
            "invalid_password_exception.password_requires_special_character",
            "Password must contain at least one special character.");
    }
}