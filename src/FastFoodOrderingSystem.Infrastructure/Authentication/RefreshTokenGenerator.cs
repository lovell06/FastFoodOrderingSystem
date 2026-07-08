using System.Security.Cryptography;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Domain.RefreshTokens;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public Token Generate()
    {
        var randomString = RandomNumberGenerator.GetHexString(256);
        var token = Convert.ToHexString(
            System.Text.Encoding.UTF8.GetBytes(randomString));

        return Token.Create(token);
    }
}