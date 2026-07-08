using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;
using FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects;

public sealed record OtpCodeHash
{
    public const int MaxLength = 256;
    public string Value { get; init; }

    private OtpCodeHash(string value)
    {
        Value = value;
    }

    private static DomainError? Validate(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return OtpCodeHashError.Empty();
        
        if (raw.Length > MaxLength)
            return OtpCodeHashError.ExceedsMaxLength(MaxLength);

        return null;
    }

    public static DomainResult<OtpCodeHash> Create(string raw)
    {
        var error = Validate(raw);

        if (error is not null)
            return DomainResult<OtpCodeHash>.Failure(error);
        
        return DomainResult<OtpCodeHash>.Success(new OtpCodeHash(raw));
    }
}