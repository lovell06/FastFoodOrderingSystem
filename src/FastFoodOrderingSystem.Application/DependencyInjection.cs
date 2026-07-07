using FastFoodOrderingSystem.Application.Features.Auth.Register;
using FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;
using Microsoft.Extensions.DependencyInjection;

namespace FastFoodOrderingSystem.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterHandler>();
        services.AddScoped<VerifyOtpHandler>();
        return services;
    }
}