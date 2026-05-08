using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

public class InvalidPasswordHashException : DomainException
{
    private InvalidPasswordHashException(string code, string message) : base(code, message)
    {
    }

    public static InvalidPasswordHashException Empty()
    {
        return new InvalidPasswordHashException(
            code: "password_hash.emtpy",
            message: "Password hash must not be empty.");
    }
    
    public static InvalidPasswordHashException ExceedsMaxLength(int maxLength)
    {
        return new InvalidPasswordHashException(
            code: "password_hash.exceeds_max_length",
            message: $"Password hash must not exceed {maxLength} characters.");
    }
}