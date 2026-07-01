using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IOtpHashService
{
    OtpCodeHash Hash(OtpCode code);
    bool Verify(OtpCode otpCode, OtpCodeHash otpCodeHash);
}