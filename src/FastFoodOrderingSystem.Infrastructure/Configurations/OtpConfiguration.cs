using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Configurations;

public sealed class OtpConfiguration : IOtpConfiguration
{
    public OtpConfiguration(IOptions<OtpOption> options)
    {
        Expiration = options.Value.Expiration;
        Length = options.Value.Length;
        MaxAttemptCount = options.Value.MaxAttemptCount;
    }

    public int Expiration { get; init; }

    public int Length { get; init; }

    public int MaxAttemptCount { get; init; }
}