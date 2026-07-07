using System.Text.Json;
using FastFoodOrderingSystem.Infrastructure.Serialization.Enums;
using FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Infrastructure.Serialization;

public static class DependencyInjection
{
    public static IServiceCollection AddSerialization(this IServiceCollection services)
    {
        services.AddSingleton(_ =>
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
        return services;
    }
}