using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.ForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.Login;
using FastFoodOrderingSystem.Application.Features.Auth.Logout;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh;
using FastFoodOrderingSystem.Application.Features.Auth.Register;
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

        services.AddScoped<VerifyRegisterHandler>();
        services.AddScoped(sp =>
        {
            IHandler<VerifyRegisterCommand, Result<Unit>> handler = sp.GetRequiredService<VerifyRegisterHandler>();

            handler = new TransactionCommandDecorator<VerifyRegisterCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>());
            handler = new PerformanceHandlerDecorator<VerifyRegisterCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<VerifyRegisterCommand, Result<Unit>>>>());
            handler = new LoggingHandlerDecorator<VerifyRegisterCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<VerifyRegisterCommand, Result<Unit>>>>());

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
            IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> handler =
                sp.GetRequiredService<RefreshTokenHandler>();

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

        services.AddScoped<ForgotPasswordHandler>();
        services.AddScoped(sp =>
        {
            IHandler<ForgotPasswordCommand, Result<Unit>> handler =
                sp.GetRequiredService<ForgotPasswordHandler>();

            handler = new TransactionCommandDecorator<ForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>());

            handler = new PerformanceHandlerDecorator<ForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<ForgotPasswordCommand, Result<Unit>>>>());

            handler = new LoggingHandlerDecorator<ForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<ForgotPasswordCommand, Result<Unit>>>>());

            return handler;
        });

        services.AddScoped<VerifyForgotPasswordHandler>();
        services.AddScoped(sp =>
        {
            IHandler<VerifyForgotPasswordCommand, Result<Unit>> handler =
                sp.GetRequiredService<VerifyForgotPasswordHandler>();

            handler = new TransactionCommandDecorator<VerifyForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>());

            handler = new PerformanceHandlerDecorator<VerifyForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<VerifyForgotPasswordCommand, Result<Unit>>>>());

            handler = new LoggingHandlerDecorator<VerifyForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<VerifyForgotPasswordCommand, Result<Unit>>>>());

            return handler;
        });

        return services;
    }
}