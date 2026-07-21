using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;
using FastFoodOrderingSystem.Application.Features.Auth.CompleteForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.InitiateForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.Login;
using FastFoodOrderingSystem.Application.Features.Auth.Logout;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth;

internal static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationHandlers(this IServiceCollection services)
    {
        services.AddScoped<LoginHandler>();
        services.AddScoped(sp =>
        {
            IHandler<LoginCommand, Result<LoginResponse>> handler = sp.GetRequiredService<LoginHandler>();

            handler = new TransactionCommandDecorator<LoginCommand, Result<LoginResponse>>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<LoginCommand, Result<LoginResponse>>>>());

            handler = new PerformanceHandlerDecorator<LoginCommand, Result<LoginResponse>>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<LoginCommand, Result<LoginResponse>>>>());

            handler = new LoggingHandlerDecorator<LoginCommand, Result<LoginResponse>>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<LoginCommand, Result<LoginResponse>>>>());

            return handler;
        });

        services.AddScoped<LogoutHandler>();
        services.AddScoped(sp =>
        {
            IHandler<LogoutCommand, Result<Unit>> handler = sp.GetRequiredService<LogoutHandler>();

            handler = new TransactionCommandDecorator<LogoutCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<LogoutCommand, Result<Unit>>>>());

            handler = new PerformanceHandlerDecorator<LogoutCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<LogoutCommand, Result<Unit>>>>());

            handler = new LoggingHandlerDecorator<LogoutCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<LogoutCommand, Result<Unit>>>>());

            return handler;
        });

        services.AddScoped<RefreshTokenHandler>();
        services.AddScoped(sp =>
        {
            IHandler<RefreshTokenCommand, Result<RefreshTokenResponse>> handler =
                sp.GetRequiredService<RefreshTokenHandler>();

            handler = new TransactionCommandDecorator<RefreshTokenCommand, Result<RefreshTokenResponse>>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<RefreshTokenCommand, Result<RefreshTokenResponse>>>>());

            handler = new PerformanceHandlerDecorator<RefreshTokenCommand, Result<RefreshTokenResponse>>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<RefreshTokenCommand, Result<RefreshTokenResponse>>>>());

            handler = new LoggingHandlerDecorator<RefreshTokenCommand, Result<RefreshTokenResponse>>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<RefreshTokenCommand, Result<RefreshTokenResponse>>>>());

            return handler;
        });

        services.AddScoped<InitiateForgotPasswordHandler>();
        services.AddScoped(sp =>
        {
            IHandler<InitiateForgotPasswordCommand, Result<Unit>> handler =
                sp.GetRequiredService<InitiateForgotPasswordHandler>();

            handler = new TransactionCommandDecorator<InitiateForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<InitiateForgotPasswordCommand, Result<Unit>>>>());

            handler = new PerformanceHandlerDecorator<InitiateForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<InitiateForgotPasswordCommand, Result<Unit>>>>());

            handler = new LoggingHandlerDecorator<InitiateForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<InitiateForgotPasswordCommand, Result<Unit>>>>());

            return handler;
        });

        services.AddScoped<CompleteForgotPasswordHandler>();
        services.AddScoped(sp =>
        {
            IHandler<CompleteForgotPasswordCommand, Result<Unit>> handler =
                sp.GetRequiredService<CompleteForgotPasswordHandler>();

            handler = new TransactionCommandDecorator<CompleteForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<CompleteForgotPasswordCommand, Result<Unit>>>>());

            handler = new PerformanceHandlerDecorator<CompleteForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<CompleteForgotPasswordCommand, Result<Unit>>>>());

            handler = new LoggingHandlerDecorator<CompleteForgotPasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<CompleteForgotPasswordCommand, Result<Unit>>>>());

            return handler;
        });

        services.AddScoped<ChangePasswordHandler>();
        services.AddScoped(sp =>
        {
            IHandler<ChangePasswordCommand, Result<Unit>> handler = sp.GetRequiredService<ChangePasswordHandler>();

            handler = new TransactionCommandDecorator<ChangePasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<ChangePasswordCommand, Result<Unit>>>>());

            handler = new PerformanceHandlerDecorator<ChangePasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<ChangePasswordCommand, Result<Unit>>>>());

            handler = new LoggingHandlerDecorator<ChangePasswordCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<ChangePasswordCommand, Result<Unit>>>>());

            return handler;
        });

        return services;
    }
}