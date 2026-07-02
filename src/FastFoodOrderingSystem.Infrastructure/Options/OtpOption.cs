namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed record OtpOption(string SecretKey, int Expiration)
{
    public const string SectionName = "OtpOption";
}