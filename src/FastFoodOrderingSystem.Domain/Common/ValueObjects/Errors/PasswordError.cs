using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public sealed class PasswordError
{
    public static DomainError Empty()
    {
        return new DomainError(
            "password.empty",
            "Password must not be empty.");
    }

    public static DomainError PasswordLengthBelowMinimum(int minLength)
    {
        return new DomainError(
            "password.length_below_minimum",
            $"Password must be at least {minLength} characters.");
    }

    public static DomainError PasswordRequiresUppercase()
    {
        return new DomainError(
            "password.requires_uppercase",
            "Password must contain at least one uppercase letter.");
    }

    public static DomainError PasswordRequiresLowercase()
    {
        return new DomainError(
            "password.requires_lowercase",
            "Password must contain at least one lowercase letter.");
    }

    public static DomainError PasswordRequiresDigit()
    {
        return new DomainError(
            "password.requires_digit",
            "Password must contain at least one digit.");
    }

    public static DomainError PasswordRequiresSpecialCharacter()
    {
        return new DomainError(
            "password.requires_special_character",
            "Password must contain at least one special character.");
    }
}