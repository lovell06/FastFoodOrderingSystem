using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record OtpCodeHash
{
    public const int MaxLength = 256;
    public string Value { get; }

    private OtpCodeHash(string value)
    {
        Value = value;
    }

    private static void Validate(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw InvalidOtpCodeHashException.Empty();
        
        if (raw.Length > MaxLength)
            throw InvalidOtpCodeHashException.ExceedsMaxLength(MaxLength);
    }

    public static OtpCodeHash Create(string raw)
    {
        Validate(raw);
        return new OtpCodeHash(raw);
    }
}