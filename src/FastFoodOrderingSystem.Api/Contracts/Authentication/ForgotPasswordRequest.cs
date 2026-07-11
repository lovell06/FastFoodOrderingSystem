using FastFoodOrderingSystem.Application.Features.Auth.ForgotPassword;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record ForgotPasswordRequest(
    string Email)
{
    public ForgotPasswordCommand ToCommand()
    {
        return new ForgotPasswordCommand(Email);
    }
}