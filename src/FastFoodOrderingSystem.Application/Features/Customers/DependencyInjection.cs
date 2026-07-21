using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Application.Features.Customers.CompleteRegistration;
using FastFoodOrderingSystem.Application.Features.Customers.InitiateRegistration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Features.Customers;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomerHandlers(this IServiceCollection services)
    {
        services.AddScoped<InitiateRegistrationHandler>();
        services.AddScoped(sp =>
        {
            IHandler<InitiateRegistrationCommand, Result<Unit>> handler = sp.GetRequiredService<InitiateRegistrationHandler>();

            handler = new TransactionCommandDecorator<InitiateRegistrationCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<InitiateRegistrationCommand, Result<Unit>>>>());
            handler = new PerformanceHandlerDecorator<InitiateRegistrationCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<InitiateRegistrationCommand, Result<Unit>>>>());
            handler = new LoggingHandlerDecorator<InitiateRegistrationCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<InitiateRegistrationCommand, Result<Unit>>>>());

            return handler;
        });

        services.AddScoped<CompleteRegistrationHandler>();
        services.AddScoped(sp =>
        {
            IHandler<CompleteRegistrationCommand, Result<Unit>> handler = sp.GetRequiredService<CompleteRegistrationHandler>();

            handler = new TransactionCommandDecorator<CompleteRegistrationCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<IUnitWork>(),
                sp.GetRequiredService<ILogger<TransactionCommandDecorator<CompleteRegistrationCommand, Result<Unit>>>>());
            handler = new PerformanceHandlerDecorator<CompleteRegistrationCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<CompleteRegistrationCommand, Result<Unit>>>>());
            handler = new LoggingHandlerDecorator<CompleteRegistrationCommand, Result<Unit>>(
                handler,
                sp.GetRequiredService<ILogger<LoggingHandlerDecorator<CompleteRegistrationCommand, Result<Unit>>>>());

            return handler;
        });
        return services;
    }
}