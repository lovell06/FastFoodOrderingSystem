using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

public sealed class PasswordHashError
{
    public static DomainError Empty()
    {
        return new DomainError(
            Code: "password_hash.emtpy",
            Message: "Password hash must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new DomainError(
            Code: "password_hash.exceeds_max_length",
            Message: $"Password hash must not exceed {maxLength} characters.");
    }
}