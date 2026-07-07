using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public static class EmailError
{
    public static DomainError Empty()
    {
        return new(
            Code: "email.empty",
            Message: "Email must not be empty!");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "email.exceeds_max_length",
            Message: $"Email must not be exceed {maxLength} characters");
    }

    public static DomainError ContainsWhitespace()
    {
        return new(
            Code: "email.contains_whitespace",
            Message: "Email must not contain whitespace chararacters!");
    }

    public static DomainError InvalidFormat()
    {
        return new(
            Code: "email.invalid_format",
            Message: "Email format is invalid!");
    }
}