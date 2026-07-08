using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Handlers.CommandDecorators;
using FastFoodOrderingSystem.Application.Common.Handlers.HandlerDecorators;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Login;
using FastFoodOrderingSystem.Application.Features.Auth.Logout;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh;
using FastFoodOrderingSystem.Application.Features.Auth.Register;
using FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationHandlers(this IServiceCollection services)
    {
        services.AddScoped<RegisterHandler>();
        services.AddScoped(sp =>
        {
            IHandler<RegisterCommand, Result<RegisterResponse>> handler = sp.GetRequiredService<RegisterHandler>();

            handler = new TransactionCommandDecorator<RegisterCommand, Result<RegisterResponse>>(
                handler,
                sp.GetRequiredService<IUnitWork>());
            handler = new PerformanceHandlerDecorator<RegisterCommand, Result<RegisterResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<RegisterCommand, Result<RegisterResponse>>>>());
            handler = new LoggingHandlerDecorator<RegisterCommand, Result<RegisterResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<RegisterCommand, Result<RegisterResponse>>>>());

            return handler;
        });

        services.AddScoped<VerifyOtpHandler>();
        services.AddScoped(sp =>
        {
            IHandler<VerifyOtpCommand, Result<VerifyOtpResponse>> handler = sp.GetRequiredService<VerifyOtpHandler>();

            handler = new TransactionCommandDecorator<VerifyOtpCommand, Result<VerifyOtpResponse>>(
                handler,
                sp.GetRequiredService<IUnitWork>());
            handler = new PerformanceHandlerDecorator<VerifyOtpCommand, Result<VerifyOtpResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<VerifyOtpCommand, Result<VerifyOtpResponse>>>>());
            handler = new LoggingHandlerDecorator<VerifyOtpCommand, Result<VerifyOtpResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<VerifyOtpCommand, Result<VerifyOtpResponse>>>>());

            return handler;
        });

        services.AddScoped<LoginHandler>();
        services.AddScoped(sp =>
        {
            IHandler<LoginCommand, Result<LoginResponse>> handler = sp.GetRequiredService<LoginHandler>();

            handler = new TransactionCommandDecorator<LoginCommand, Result<LoginResponse>>(
                handler,
                sp.GetRequiredService<IUnitWork>());

            handler = new PerformanceHandlerDecorator<LoginCommand, Result<LoginResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<LoginCommand, Result<LoginResponse>>>>());

            handler = new LoggingHandlerDecorator<LoginCommand, Result<LoginResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<LoginCommand, Result<LoginResponse>>>>());

            return handler;
        });

        services.AddScoped<LogoutHandler>();
        services.AddScoped(sp =>
        {
            IHandler<LogoutCommand, Result<LogoutResponse>> handler = sp.GetRequiredService<LogoutHandler>();

            handler = new TransactionCommandDecorator<LogoutCommand, Result<LogoutResponse>>(
                handler,
                sp.GetRequiredService<IUnitWork>());

            handler = new PerformanceHandlerDecorator<LogoutCommand, Result<LogoutResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<LogoutCommand, Result<LogoutResponse>>>>());

            handler = new LoggingHandlerDecorator<LogoutCommand, Result<LogoutResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<LogoutCommand, Result<LogoutResponse>>>>());
            
            return handler;
        });

        services.AddScoped<RefreshTokenHandler>();
        services.AddScoped(sp =>
        {
            IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> handler = sp.GetRequiredService<RefreshTokenHandler>();

            handler = new TransactionCommandDecorator<RefreshTokenCommand, Result<RefreshTokenResponse>>(
                handler,
                sp.GetRequiredService<IUnitWork>());

            handler = new PerformanceHandlerDecorator<RefreshTokenCommand, Result<RefreshTokenResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>>>());

            handler = new LoggingHandlerDecorator<RefreshTokenCommand, Result<RefreshTokenResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>>>>());

            return handler;
        });

        return services;
    }
}