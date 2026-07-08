using FastFoodOrderingSystem.Domain.Common.Abstractions;

namespace FastFoodOrderingSystem.Domain.RefreshTokens;

public class RefreshToken : AggregateRoot<TokenId>
{
    public Guid UserId { get; init; }
    public Token Token { get; init; }
    public DateTime ExpiresAt { get; init; }

    protected RefreshToken()
    {
    }

    private RefreshToken(TokenId id, Guid userId, Token token, DateTime expiresAt) : base(id)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
    }

    public static RefreshToken Create(TokenId id, Guid userId, Token token, DateTime expiresAt)
    {
        return new RefreshToken(id, userId, token, expiresAt);
    }
}