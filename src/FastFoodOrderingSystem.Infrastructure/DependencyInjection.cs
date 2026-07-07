using System.Text.Json;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Configurations;
using FastFoodOrderingSystem.Application.Abstractions.Emails;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Infrastructure.Authentication;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis;
using FastFoodOrderingSystem.Infrastructure.Cache.Redis.PendingRegistration;
using FastFoodOrderingSystem.Infrastructure.Configurations;
using FastFoodOrderingSystem.Infrastructure.Emails;
using FastFoodOrderingSystem.Infrastructure.Options;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database;
using FastFoodOrderingSystem.Infrastructure.Persistence.Repositories;
using FastFoodOrderingSystem.Infrastructure.Serialization.Enums;
using FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Time;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

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

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var option = sp.GetRequiredService<IOptions<RedisOption>>().Value;
            return ConnectionMultiplexer.Connect(option.ConnectionStrings);
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
         * Add Serializer Options
         */
        services.AddSingleton(sp  =>
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            options.Converters.Add(new AddressJsonConverter());
            options.Converters.Add(new EmailJsonConverter());
            options.Converters.Add(new FullNameJsonConverter());
            options.Converters.Add(new ImagePathJsonConverter());
            options.Converters.Add(new OtpCodeHashJsonConverter());
            options.Converters.Add(new PasswordHashJsonConverter());
            options.Converters.Add(new PhoneNumberJsonConverter());
            options.Converters.Add(new UserRoleJsonConverter());

            return options;
        });

        /*
         * Add Dependency For Services
         */
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<RedisKeyProvider>();

        /*
         * Register Services
         */
        services.AddScoped<IPasswordHashService, PasswordHashService>();
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IOtpHashService, OtpHashService>();
        services.AddScoped<IEmailSender, GmailSender>();
        services.AddScoped<IPendingRegistrationStore, RedisPendingRegistrationCache>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        /*
         * Register Repositories
         */
        services.AddScoped<IUnitWork, UnitWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        /*
         * Register Configurations
         */
        services.AddScoped<IOtpConfiguration, OtpConfiguration>();
        services.AddScoped<IEmailConfiguration, GmailConfiguration>();
        return services;
    }
}