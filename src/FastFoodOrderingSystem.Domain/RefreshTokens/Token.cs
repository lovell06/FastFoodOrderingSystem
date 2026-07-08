namespace FastFoodOrderingSystem.Domain.RefreshTokens;

public sealed record Token
{
    public string Value { get; init; }

    private Token(string value)
    {
        Value = value;
    }

    public static Token Create(string value)
    {
        return new Token(value);
    }
}