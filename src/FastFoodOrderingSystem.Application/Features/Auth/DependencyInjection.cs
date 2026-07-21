using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
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
            IHandler<LoginCommand, LoginResponse> handler = sp.GetRequiredService<LoginHandler>();

            handler = new TransactionCommandDecorator<LoginCommand, LoginResponse>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<LoginCommand, LoginResponse>>>());

            handler = new PerformanceHandlerDecorator<LoginCommand, LoginResponse>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<LoginCommand, LoginResponse>>>());

            handler = new LoggingHandlerDecorator<LoginCommand, LoginResponse>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<LoginCommand, LoginResponse>>>());

            return handler;
        });

        services.AddScoped<LogoutHandler>();
        services.AddScoped(sp =>
        {
            IHandler<LogoutCommand, Unit> handler = sp.GetRequiredService<LogoutHandler>();

            handler = new TransactionCommandDecorator<LogoutCommand, Unit>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<LogoutCommand, Unit>>>());

            handler = new PerformanceHandlerDecorator<LogoutCommand, Unit>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<LogoutCommand, Unit>>>());

            handler = new LoggingHandlerDecorator<LogoutCommand, Unit>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<LogoutCommand, Unit>>>());

            return handler;
        });

        services.AddScoped<RefreshTokenHandler>();
        services.AddScoped(sp =>
        {
            IHandler<RefreshTokenCommand, RefreshTokenResponse> handler =
                sp.GetRequiredService<RefreshTokenHandler>();

            handler = new TransactionCommandDecorator<RefreshTokenCommand, RefreshTokenResponse>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<RefreshTokenCommand, RefreshTokenResponse>>>());

            handler = new PerformanceHandlerDecorator<RefreshTokenCommand, RefreshTokenResponse>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<RefreshTokenCommand, RefreshTokenResponse>>>());

            handler = new LoggingHandlerDecorator<RefreshTokenCommand, RefreshTokenResponse>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<RefreshTokenCommand, RefreshTokenResponse>>>());

            return handler;
        });

        services.AddScoped<InitiateForgotPasswordHandler>();
        services.AddScoped(sp =>
        {
            IHandler<InitiateForgotPasswordCommand, Unit> handler =
                sp.GetRequiredService<InitiateForgotPasswordHandler>();

            handler = new TransactionCommandDecorator<InitiateForgotPasswordCommand, Unit>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<InitiateForgotPasswordCommand, Unit>>>());

            handler = new PerformanceHandlerDecorator<InitiateForgotPasswordCommand, Unit>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<InitiateForgotPasswordCommand, Unit>>>());

            handler = new LoggingHandlerDecorator<InitiateForgotPasswordCommand, Unit>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<InitiateForgotPasswordCommand, Unit>>>());

            return handler;
        });

        services.AddScoped<CompleteForgotPasswordHandler>();
        services.AddScoped(sp =>
        {
            IHandler<CompleteForgotPasswordCommand, Unit> handler =
                sp.GetRequiredService<CompleteForgotPasswordHandler>();

            handler = new TransactionCommandDecorator<CompleteForgotPasswordCommand, Unit>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<CompleteForgotPasswordCommand, Unit>>>());

            handler = new PerformanceHandlerDecorator<CompleteForgotPasswordCommand, Unit>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<CompleteForgotPasswordCommand, Unit>>>());

            handler = new LoggingHandlerDecorator<CompleteForgotPasswordCommand, Unit>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<CompleteForgotPasswordCommand, Unit>>>());

            return handler;
        });

        services.AddScoped<ChangePasswordHandler>();
        services.AddScoped(sp =>
        {
            IHandler<ChangePasswordCommand, Unit> handler = sp.GetRequiredService<ChangePasswordHandler>();

            handler = new TransactionCommandDecorator<ChangePasswordCommand, Unit>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<ChangePasswordCommand, Unit>>>());

            handler = new PerformanceHandlerDecorator<ChangePasswordCommand, Unit>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<ChangePasswordCommand, Unit>>>());

            handler = new LoggingHandlerDecorator<ChangePasswordCommand, Unit>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<ChangePasswordCommand, Unit>>>());

            return handler;
        });

        return services;
    }
}