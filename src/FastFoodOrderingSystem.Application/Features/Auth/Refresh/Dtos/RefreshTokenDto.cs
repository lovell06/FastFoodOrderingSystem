namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh.Dtos;

public sealed record RefreshTokenDto(string Token, DateTime ExpiresAt);