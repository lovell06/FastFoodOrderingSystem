using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

public static class InvalidPasswordHashError
{
    public static DomainError Empty()
    {
        return new DomainError(
            Code: "invalid_password_hash_error.empty",
            Message: "Password hash must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new DomainError(
            Code: "invalid_password_hash_error.exceeds_max_length",
            Message: $"Password hash must not exceed {maxLength} characters.");
    }
}