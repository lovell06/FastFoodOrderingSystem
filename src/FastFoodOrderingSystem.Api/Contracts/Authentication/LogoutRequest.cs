using FastFoodOrderingSystem.Application.Features.Auth.Logout;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record LogoutRequest(string RefreshToken)
{
    public LogoutCommand ToCommand()
    {
        return new LogoutCommand(RefreshToken);
    }
}