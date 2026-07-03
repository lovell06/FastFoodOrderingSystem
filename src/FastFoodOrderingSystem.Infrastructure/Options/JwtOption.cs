namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class JwtOption
{
    public const string SectionName = "JwtOption";
    public string Issuer { get; }
    public string Audience { get; }
    public string Key { get; }
    public int ExpireMinutes { get; }
}