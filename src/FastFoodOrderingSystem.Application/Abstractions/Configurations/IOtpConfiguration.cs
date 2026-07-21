namespace FastFoodOrderingSystem.Application.Abstractions.Configurations;

public interface IOtpConfiguration
{
    int Expiration { get; }
    int Length { get; }
    int MaxAttemptCount { get; }
}