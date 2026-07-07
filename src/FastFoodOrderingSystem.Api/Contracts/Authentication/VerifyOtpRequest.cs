using FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record VerifyOtpRequest(string Email, string OtpCode)
{
    public VerifyOtpCommand ToCommand()
    {
        return new(Email: Email, Code: OtpCode);
    }
}