using System.Security.Cryptography;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public sealed class RefreshTokenGenerator : IRefreshTokenGenerator
{
    public string Generate()
    {
        var randomString = RandomNumberGenerator.GetHexString(256);
        
        return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(randomString))
            .TrimEnd('=');
    }
}