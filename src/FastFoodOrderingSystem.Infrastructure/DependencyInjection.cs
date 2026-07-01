using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Infrastructure.Authentication;
using FastFoodOrderingSystem.Infrastructure.Options;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database;
using FastFoodOrderingSystem.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        /*
         * DB Configuration
         */
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        /*
         * Options Configuration
         */
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

        /*
         * Add Dependency For Services
         */
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        /*
         * Register Services
         */
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<IOtpHashService, OtpHashService>();

        /*
         * Register Repositories
         */
        services.AddScoped<IUnitWork, UnitWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}