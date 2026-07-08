using FastFoodOrderingSystem.Application.Features.Auth.Refresh.Dtos;

namespace FastFoodOrderingSystem.Application.Features.Auth.Refresh;

public sealed record RefreshTokenResponse(AccessTokenDto AccessTokenInfo, RefreshTokenDto RefreshTokenInfo);