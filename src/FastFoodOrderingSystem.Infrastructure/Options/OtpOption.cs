namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class OtpOption
{
    public const string SectionName = "OtpOption";
    public string SecretKey { get; }
    public int Expiration { get; }
    public int Length { get; }
    public int MaxAttemptCount { get; }
}