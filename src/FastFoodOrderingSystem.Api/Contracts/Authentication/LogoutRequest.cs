using FastFoodOrderingSystem.Application.Features.Auth.Logout;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record LogoutRequest(string RefreshTokenId)
{
    public LogoutCommand ToCommand()
    {
        return new LogoutCommand(RefreshTokenId);
    }
}