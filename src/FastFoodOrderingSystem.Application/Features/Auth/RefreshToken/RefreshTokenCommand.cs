namespace FastFoodOrderingSystem.Application.Features.Auth.RefreshToken;

public sealed record RefreshTokenCommand(Guid UserId, string RefreshToken);