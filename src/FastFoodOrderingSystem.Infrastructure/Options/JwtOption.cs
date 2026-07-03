namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class JwtOption
{
    public const string SectionName = "JwtOption";
    public required string Issuer { get; init; } 
    public required string Audience { get; init; } 
    public required string Key { get; init; } 
    public int ExpireMinutes { get; init; }
}