using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

public static class InvalidEmailError
{
    public static DomainError Empty()
    {
        return new(
            Code: "invalid_email_error.empty",
            Message: "Email must not be empty!");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "invalid_email_error.exceeds_max_length",
            Message: $"Email must not be exceed {maxLength} characters");
    }

    public static DomainError ContainsWhitespace()
    {
        return new(
            Code: "invalid_email_error.contains_whitespace",
            Message: "Email must not contain whitespace characters!");
    }

    public static DomainError InvalidFormat()
    {
        return new(
            Code: "invalid_email_error.invalid_format",
            Message: "Email format is invalid!");
    }
}