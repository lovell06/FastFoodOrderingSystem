using FastFoodOrderingSystem.Application.Features.Auth.ForgotPassword;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record VerifyForgotPasswordRequest(string Email, string OtpCode)
{
    public VerifyForgotPasswordCommand ToCommand()
    {
        return new VerifyForgotPasswordCommand(Email, OtpCode);
    }
}