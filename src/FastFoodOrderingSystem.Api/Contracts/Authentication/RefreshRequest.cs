using FastFoodOrderingSystem.Application.Features.Auth.Refresh;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record RefreshRequest(Guid UserId, string RefreshToken)
{
    public RefreshTokenCommand ToCommand()
    {
        return new RefreshTokenCommand(UserId, RefreshToken);
    }
}