using System.Security.Cryptography;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public RefreshToken Generate(User user, DateTime expiresAt)
    {
        var randomString = RandomNumberGenerator.GetHexString(256);
        var token = Convert.ToBase64String(
            System.Text.Encoding.UTF8.GetBytes(randomString));

        return RefreshToken.Create(user.Id, token, expiresAt);
    }
}