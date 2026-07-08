using System.Security.Cryptography;

namespace FastFoodOrderingSystem.Domain.RefreshTokens;

public sealed record TokenId
{
    public string Value { get; init; }
    private TokenId(string value)
    {
        Value = value;
    }

    public static TokenId Create(Token id)
    {
        var hashBytes = SHA256.HashData(
            System.Text.Encoding.UTF8.GetBytes(id.Value));

        var hashHex = Convert.ToHexString(hashBytes);

        return new TokenId(hashHex);
    }
}