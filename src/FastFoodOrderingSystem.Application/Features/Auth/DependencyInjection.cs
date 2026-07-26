using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Features.Auth.ChangePassword;
using FastFoodOrderingSystem.Application.Features.Auth.CompleteForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.InitiateForgotPassword;
using FastFoodOrderingSystem.Application.Features.Auth.Login;
using FastFoodOrderingSystem.Application.Features.Auth.Logout;
using FastFoodOrderingSystem.Application.Features.Auth.LogoutAllDevices;
using FastFoodOrderingSystem.Application.Features.Auth.Refresh;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Auth;

internal static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationHandlers(this IServiceCollection services)
    {
        services.AddCommandHandler<LoginCommand, LoginResponse, LoginHandler>();
        services.AddCommandHandler<LogoutCommand, Unit, LogoutHandler>();
        services.AddCommandHandler<RefreshTokenCommand, RefreshTokenResponse, RefreshTokenHandler>();
        services.AddCommandHandler<InitiateForgotPasswordCommand, Unit, InitiateForgotPasswordHandler>();
        services.AddCommandHandler<CompleteForgotPasswordCommand, Unit, CompleteForgotPasswordHandler>();
        services.AddCommandHandler<ChangePasswordCommand, Unit, ChangePasswordHandler>();
        services.AddCommandHandler<LogoutAllDevicesCommand, Unit, LogoutAllDevicesHandler>();

        return services;
    }
}