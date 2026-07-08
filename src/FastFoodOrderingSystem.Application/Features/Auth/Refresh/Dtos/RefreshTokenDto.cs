namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh.Dtos;

public sealed record RefreshTokenDto(Guid Id, string Token, DateTime ExpiresAt);