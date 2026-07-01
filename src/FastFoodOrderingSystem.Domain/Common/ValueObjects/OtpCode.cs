using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record OtpCode
{
    public const int Length = 6;
    public string Value { get; }

    private OtpCode(string value)
    {
        Value = value;
    }

    private static void Validate(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            throw InvalidOtpCodeException.Empty();
        
        if (raw.Length != Length)
            throw InvalidOtpCodeException.InvalidLength(Length);
        
        if (!raw.All(char.IsDigit))
            throw InvalidOtpCodeException.CodeIsNotDigit();
    }

    public static OtpCode Create(string raw)
    {
        Validate(raw);
        return new OtpCode(raw);
    }
}