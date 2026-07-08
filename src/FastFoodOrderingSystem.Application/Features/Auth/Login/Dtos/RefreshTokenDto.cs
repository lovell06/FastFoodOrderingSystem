namespace FastFoodOrderingSystem.Application.Features.Auth.Login.Dtos;

public record RefreshTokenDto(Guid UserId, string Token, DateTime ExpiresAt);