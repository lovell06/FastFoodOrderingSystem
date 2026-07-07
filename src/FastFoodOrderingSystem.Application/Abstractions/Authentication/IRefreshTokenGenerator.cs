using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IRefreshTokenGenerator
{
    RefreshToken Generate(User user, DateTime expiresAt);
}