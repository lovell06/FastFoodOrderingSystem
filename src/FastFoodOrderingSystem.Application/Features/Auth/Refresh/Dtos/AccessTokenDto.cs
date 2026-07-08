namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh.Dtos;

public sealed record AccessTokenDto(string Token, DateTime ExpiresAt);