namespace FastFoodOrderingSystem.Domain.RefreshTokens;

public sealed record TokenId
{
    public Guid Value { get; init; }
    private TokenId(Guid value)
    {
        Value = value;
    }

    public static TokenId Create(Guid id)
    {
        return new TokenId(id);
    }
}