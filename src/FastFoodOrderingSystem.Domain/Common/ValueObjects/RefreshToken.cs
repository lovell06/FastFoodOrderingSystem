namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record RefreshToken
{
    public Guid UserId { get; init; }
    public string Token { get; init; }
    public DateTime ExpiresAt { get; init; }
    private RefreshToken(Guid userId, string token, DateTime expiresAt)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
    }

    public static RefreshToken Create(Guid userId, string token, DateTime expiresAt)
    {
        return new RefreshToken(userId, token, expiresAt);
    }
}