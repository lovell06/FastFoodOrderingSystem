using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

public sealed class InvalidEmailException : DomainException
{
    private InvalidEmailException(string code, string message) : base(code, message)
    {
    }

    public static InvalidEmailException Empty()
    {
        return new InvalidEmailException(
            code: "email.empty", 
            message: "Email must not be empty!");
    }

    public static InvalidEmailException ExceedsMaxLength(int maxLength)
    {
        return new InvalidEmailException(
            code: "email.exceeds_max_length",
            message: $"Email must not be exceed {maxLength} characters");
    }

    public static InvalidEmailException ContainsWhitespace()
    {
        return new InvalidEmailException(
            code: "email.contains_whitespace",
            message: "Email must not contain whitespace chararacters!");
    }

    public static InvalidEmailException InvalidFormat()
    {
        return new InvalidEmailException(
            code: "email.invalid_format",
            message: "Email format is invalid!");
    }
}