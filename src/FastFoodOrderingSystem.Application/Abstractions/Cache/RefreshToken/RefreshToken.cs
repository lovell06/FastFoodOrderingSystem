using System.Security.Cryptography;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache.RefreshToken;

public sealed class RefreshToken
{
    public string Id { get; init; }
    public Guid UserId { get; init; }
    public string Token { get; init; }
    public DateTime ExpiresAt { get; init; }

    private RefreshToken(string id, Guid userId, string token, DateTime expiresAt)
    {
        Id = id;
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
    }
    
    public static RefreshToken Create(Guid userId, string token, DateTime expiresAt)
    {
        var id = GenerateId(token);

        return new RefreshToken(id, userId, token, expiresAt);
    }

    public static string GenerateId(string token)
    {
        var bytes = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(token));

        return Convert.ToHexStringLower(bytes);
    }
}