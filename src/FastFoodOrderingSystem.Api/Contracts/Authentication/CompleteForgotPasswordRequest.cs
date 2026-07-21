using FastFoodOrderingSystem.Application.Features.Auth.CompleteForgotPassword;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record CompleteForgotPasswordRequest(string Email, string OtpCode)
{
    public CompleteForgotPasswordCommand ToCommand()
    {
        return new CompleteForgotPasswordCommand(Email, OtpCode);
    }
}