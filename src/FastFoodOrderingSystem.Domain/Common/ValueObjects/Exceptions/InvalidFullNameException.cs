using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

public sealed class InvalidFullNameException : DomainException
{
    private InvalidFullNameException(string code, string message) : base(code, message)
    {
    }

    public static InvalidFullNameException Empty()
    {
        return new InvalidFullNameException(
            code: "full_name.empty", 
            message: "Full name must not be empty!");
    }

    public static InvalidFullNameException ExceedsMaxLength(int maxLength)
    {
        return new InvalidFullNameException(
            code: "full_name.exceeds_max_length", 
            message: $"Full name must not exceed {maxLength} characters");
    }

    public static InvalidFullNameException ContainsInvalidCharacters()
    {
        return new InvalidFullNameException(
            code: "full_name.invalid_characters",
            message: "Full name contains invalid characters, only contains letters or numbers!");
    }
}