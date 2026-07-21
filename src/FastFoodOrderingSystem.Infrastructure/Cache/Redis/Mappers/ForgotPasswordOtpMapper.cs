using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.Snapshots;

using ForgotPasswordOtpCache = FastFoodOrderingSystem.Application.Abstractions.Cache.ForgotPasswordOtp;

namespace FastFoodOrderingSystem.Infrastructure.Cache.Redis.Mappers;

public static class ForgotPasswordOtpMapper
{
    public static ForgotPasswordOtpCache.ForgotPasswordOtp ToEntity(ForgotPasswordOtpSnapshot snapshot)
    {
        var emailResult = Email.Create(snapshot.Email);
        var otpCodeHashResult = OtpCodeHash.Create(snapshot.OtpCodeHash);

        if (emailResult.IsFailure || otpCodeHashResult.IsFailure)
            throw new InvalidOperationException
                ("Cannot convert snapshot -> entity. ForgotPasswordOtpSnapshot data invalid.");

        return ForgotPasswordOtpCache.ForgotPasswordOtp.Create(emailResult.Value!, otpCodeHashResult.Value!, snapshot.ExpiresAt);
    }

    public static ForgotPasswordOtpSnapshot ToSnapshot(ForgotPasswordOtpCache.ForgotPasswordOtp forgotPasswordOtp)
    {
        return new ForgotPasswordOtpSnapshot(
            Email: forgotPasswordOtp.Email.Value, 
            OtpCodeHash: forgotPasswordOtp.CodeHash.Value,
            ExpiresAt: forgotPasswordOtp.ExpiresAt);
    }
}