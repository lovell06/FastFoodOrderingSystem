using FastFoodOrderingSystem.Application.Features.Auth.RefreshToken.Dtos;

namespace FastFoodOrderingSystem.Application.Features.Auth.RefreshToken;

public sealed record RefreshTokenResponse(AccessTokenDto AccessTokenInfo, RefreshTokenDto RefreshTokenInfo);