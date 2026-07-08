namespace FastFoodOrderingSystem.Application.Features.Auth.RefreshToken.Dtos;

public sealed record RefreshTokenDto(Guid Id, string Token, DateTime ExpiresAt);