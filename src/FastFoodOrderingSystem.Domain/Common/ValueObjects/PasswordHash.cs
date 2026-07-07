using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record PasswordHash
{
    public const int MaxLength = 256;
    public string Value { get; init; }

    private PasswordHash(string value)
    {
        Value = value;
    }

    public static DomainResult<PasswordHash> Create(string passwordHash)
    {
        var error = Validate(passwordHash);
        
        if (error is not null)
            return DomainResult<PasswordHash>.Failure(error);
        
        return DomainResult<PasswordHash>.Success(new PasswordHash(passwordHash));
    }

    private static DomainError? Validate(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            return PasswordHashError.Empty();

        if (passwordHash.Length > MaxLength)
            return  PasswordHashError.ExceedsMaxLength(MaxLength);

        return null;
    }
}