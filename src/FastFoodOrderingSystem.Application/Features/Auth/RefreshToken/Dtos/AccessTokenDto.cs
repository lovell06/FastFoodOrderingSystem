namespace FastFoodOrderingSystem.Application.Features.Auth.RefreshToken.Dtos;

public sealed record AccessTokenDto(string Token, DateTime ExpiresAt);