using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Configurations;

public sealed class AccessTokenConfiguration : IAccessTokenConfiguration
{
    public AccessTokenConfiguration(IOptions<JwtOption> options)
    {
        ExpireMinutes = options.Value.ExpireMinutes;
    }
    public int ExpireMinutes { get; init; }
}