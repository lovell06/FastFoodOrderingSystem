using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public record PasswordHash
{
    public const int MaxLength = 256;
    public string Value { get; }

    private PasswordHash(string value)
    {
        Value = value;
    }

    public static PasswordHash Create(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw InvalidPasswordHashException.Empty();

        if (passwordHash.Length > MaxLength)
            throw InvalidPasswordHashException.ExceedsMaxLength(MaxLength);
        
        return new PasswordHash(passwordHash);
    }
}