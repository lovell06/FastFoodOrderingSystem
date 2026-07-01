namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed record OtpOption(string SecretKey)
{
    public const string SectionName = "OtpOption";
}