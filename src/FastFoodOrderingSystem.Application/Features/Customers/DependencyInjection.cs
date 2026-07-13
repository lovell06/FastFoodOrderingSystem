using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Customers.Register;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Customers;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomerHandlers(this IServiceCollection services)
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

        return services;
    }
}