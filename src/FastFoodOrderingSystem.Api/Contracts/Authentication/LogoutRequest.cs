using FastFoodOrderingSystem.Application.Features.Auth.Logout;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record LogoutRequest(string RefreshToken)
{
    public LogoutCommand ToCommand(Guid userId)
    {
        return new LogoutCommand(userId, RefreshToken);
    }
}