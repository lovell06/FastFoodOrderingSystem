namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed record JwtOption(
    string Issuer,
    string Audience,
    string Key,
    int ExpireMinutes)
{
    public const string SectionName = "JwtOption";
}