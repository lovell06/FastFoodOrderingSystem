namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class JwtOption
{
    public const string SectionName = "JwtOption";
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string Key { get; init; } = string.Empty;
    public int ExpireMinutes { get; init; }
}