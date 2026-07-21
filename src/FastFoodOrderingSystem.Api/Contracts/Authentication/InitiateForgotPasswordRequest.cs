using FastFoodOrderingSystem.Application.Features.Auth.InitiateForgotPassword;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record InitiateForgotPasswordRequest(
    string Email)
{
    public InitiateForgotPasswordCommand ToCommand()
    {
        return new InitiateForgotPasswordCommand(Email);
    }
}