namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class JwtOption
{
    public const string SectionName = "JwtOption";
    public string Issuer { get; init; } 
    public string Audience { get; init; } 
    public string Key { get; init; } 
    public int ExpireMinutes { get; init; }
}