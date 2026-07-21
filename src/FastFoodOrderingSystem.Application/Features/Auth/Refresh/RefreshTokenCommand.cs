namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh;

public sealed record RefreshTokenCommand(Guid UserId, string RefreshToken);