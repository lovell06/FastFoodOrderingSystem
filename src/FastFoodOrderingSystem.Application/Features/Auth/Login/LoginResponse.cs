using FastFoodOrderingSystem.Application.Features.Auth.Login.Dtos;

namespace FastFoodOrderingSystem.Application.Features.Auth.Login;

public sealed record LoginResponse(string AccessToken, RefreshTokenDto RefreshTokenInfo, UserDto UserInfo);