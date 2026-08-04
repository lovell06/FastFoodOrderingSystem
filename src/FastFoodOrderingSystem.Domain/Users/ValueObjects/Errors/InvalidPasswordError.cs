using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

public static class InvalidPasswordError
{
    public static DomainError Empty()
    {
        return new DomainError(
            "invalid_password_error.empty",
            "Password must not be empty.");
    }

    public static DomainError PasswordLengthBelowMinimum(int minLength)
    {
        return new DomainError(
            "invalid_password_error.length_below_minimum",
            $"Password must be at least {minLength} characters.");
    }

    public static DomainError PasswordRequiresUppercase()
    {
        return new DomainError(
            "invalid_password_error.requires_uppercase",
            "Password must contain at least one uppercase letter.");
    }

    public static DomainError PasswordRequiresLowercase()
    {
        return new DomainError(
            "invalid_password_error.requires_lowercase",
            "Password must contain at least one lowercase letter.");
    }

    public static DomainError PasswordRequiresDigit()
    {
        return new DomainError(
            "invalid_password_error.requires_digit",
            "Password must contain at least one digit.");
    }

    public static DomainError PasswordRequiresSpecialCharacter()
    {
        return new DomainError(
            "invalid_password_error.requires_special_character",
            "Password must contain at least one special character.");
    }
}