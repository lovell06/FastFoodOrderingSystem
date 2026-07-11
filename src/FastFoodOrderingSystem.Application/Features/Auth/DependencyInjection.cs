using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
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
            IHandler<RegisterCommand, Result<Unit>> handler = sp.GetRequiredService<RegisterHandler>();

            handler = new TransactionCommandDecorator<RegisterCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>());
            handler = new PerformanceHandlerDecorator<RegisterCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<RegisterCommand, Result<Unit>>>>());
            handler = new LoggingHandlerDecorator<RegisterCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<RegisterCommand, Result<Unit>>>>());

            return handler;
        });

        services.AddScoped<VerifyOtpHandler>();
        services.AddScoped(sp =>
        {
            IHandler<VerifyOtpCommand, Result<Unit>> handler = sp.GetRequiredService<VerifyOtpHandler>();

            handler = new TransactionCommandDecorator<VerifyOtpCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>());
            handler = new PerformanceHandlerDecorator<VerifyOtpCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<VerifyOtpCommand, Result<Unit>>>>());
            handler = new LoggingHandlerDecorator<VerifyOtpCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<VerifyOtpCommand, Result<Unit>>>>());

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
            IHandler<LogoutCommand, Result<Unit>> handler = sp.GetRequiredService<LogoutHandler>();

            handler = new TransactionCommandDecorator<LogoutCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>());

            handler = new PerformanceHandlerDecorator<LogoutCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<LogoutCommand, Result<Unit>>>>());

            handler = new LoggingHandlerDecorator<LogoutCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<LogoutCommand, Result<Unit>>>>());
            
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