using FastFoodOrderingSystem.Domain.RefreshTokens;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IRefreshTokenGenerator
{
    Token Generate();
}