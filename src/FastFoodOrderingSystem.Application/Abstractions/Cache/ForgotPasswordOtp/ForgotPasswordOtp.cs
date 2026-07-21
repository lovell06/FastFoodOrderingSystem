using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache.ForgotPasswordOtp;

public class ForgotPasswordOtp
{
    public Email Email { get; init; }
    public OtpCodeHash CodeHash { get; init; }
    public DateTime ExpiresAt { get; init; }

    private ForgotPasswordOtp(Email email, OtpCodeHash codeHash, DateTime expiresAt)
    {
        Email = email;
        CodeHash = codeHash;
        ExpiresAt = expiresAt;
    }

    public static ForgotPasswordOtp Create(Email email, OtpCodeHash codeHash, DateTime expriresAt)
    {
        return new ForgotPasswordOtp(email, codeHash, expriresAt);
    }
}