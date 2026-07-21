using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Configurations;

public sealed class RefreshTokenConfiguration : IRefreshTokenConfiguration
{
    public RefreshTokenConfiguration(IOptions<RefreshTokenOption> options)
    {
        ExpireDays = options.Value.ExpireDays;
    }
    public int ExpireDays { get; init; }
}