namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed record OtpOption(
    string SecretKey, 
    int Expiration,
    int Length,
    int MaxAttemptCount)
{
    public const string SectionName = "OtpOption";
}