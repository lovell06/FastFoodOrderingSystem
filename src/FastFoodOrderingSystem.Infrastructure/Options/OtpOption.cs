namespace FastFoodOrderingSystem.Infrastructure.Options;

public sealed class OtpOption
{
    public const string SectionName = "OtpOption";
    public string SecretKey { get; init; } = string.Empty;
    public int Expiration { get; init; } 
    public int Length { get; init; }
    public int MaxAttemptCount { get; init; }
}