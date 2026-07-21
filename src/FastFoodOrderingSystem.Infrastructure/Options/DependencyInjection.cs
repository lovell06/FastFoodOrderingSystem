using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Options;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<OtpOption>()
            .Bind(configuration.GetSection(OtpOption.SectionName))
            .ValidateOnStart();
        services.AddOptions<JwtOption>()
            .Bind(configuration.GetSection(JwtOption.SectionName))
            .ValidateOnStart();
        services.AddOptions<EmailOption>()
            .Bind(configuration.GetSection(EmailOption.SectionName))
            .ValidateOnStart();
        services.AddOptions<RedisOption>()
            .Bind(configuration.GetSection(RedisOption.SectionName))
            .ValidateOnStart();
        services.AddOptions<RefreshTokenOption>()
            .Bind(configuration.GetSection(RefreshTokenOption.SectionName))
            .ValidateOnStart();
        services.AddOptions<RandomPasswordOption>()
            .Bind(configuration.GetSection(RandomPasswordOption.SectionName))
            .ValidateOnStart();
        
        services.AddOptions<OutboxWorkerOption>();
        services.AddOptions<OutboxCleanupWorkerOption>();

        return services;
    }
}