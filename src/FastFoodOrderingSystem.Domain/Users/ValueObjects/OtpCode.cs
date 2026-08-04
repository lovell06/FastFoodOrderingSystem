using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;
using FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects;

public record struct OtpCode
{
    public const int Length = 6;
    public string Value { get; init; }

    private OtpCode(string value)
    {
        Value = value;
    }

    private static DomainError? Validate(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return InvalidOtpCodeError.Empty();
        
        if (raw.Length != Length)
            return InvalidOtpCodeError.InvalidLength(Length);
        
        if (!raw.All(char.IsDigit))
            return InvalidOtpCodeError.CodeIsNotDigit();

        return null;
    }

    public static DomainResult<OtpCode> Create(string raw)
    {
        var error = Validate(raw);

        if (error is not null)
            return DomainResult<OtpCode>.Failure(error);
        return DomainResult<OtpCode>.Success(new OtpCode(raw));
    }
}