using FastFoodOrderingSystem.Api.Middlewares;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Handlers;
using FastFoodOrderingSystem.Application.Common.Handlers.CommandDecorators;
using FastFoodOrderingSystem.Application.Common.Handlers.HandlerDecorators;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Auth.Register;
using FastFoodOrderingSystem.Application.Features.Auth.VerifyOtp;

namespace FastFoodOrderingSystem.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddScoped(sp =>
        {
            IHandler<RegisterCommand, Result<RegisterResponse>> handler = sp.GetRequiredService<RegisterHandler>();

            handler = new TransactionCommandDecorator<RegisterCommand, Result<RegisterResponse>>(
                handler,
                sp.GetRequiredService<IUnitWork>());
            handler = new PerformanceHandlerDecorator<RegisterCommand, Result<RegisterResponse>>(handler);
            handler = new LoggingHandlerDecorator<RegisterCommand, Result<RegisterResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<RegisterCommand, Result<RegisterResponse>>>>());
            
            return handler;
        });

        services.AddScoped(sp =>
        {
            IHandler<VerifyOtpCommand, Result<VerifyOtpResponse>> handler = sp.GetRequiredService<VerifyOtpHandler>();

            handler = new TransactionCommandDecorator<VerifyOtpCommand, Result<VerifyOtpResponse>>(
                handler,
                sp.GetRequiredService<IUnitWork>());
            handler = new PerformanceHandlerDecorator<VerifyOtpCommand, Result<VerifyOtpResponse>>(handler);
            handler = new LoggingHandlerDecorator<VerifyOtpCommand, Result<VerifyOtpResponse>>(
                handler,
                sp.GetRequiredService<ILogger<IHandler<VerifyOtpCommand, Result<VerifyOtpResponse>>>>());

            return handler;
        });

        services.AddScoped<ExceptionMiddleware>();
        
        return services;
    }
}