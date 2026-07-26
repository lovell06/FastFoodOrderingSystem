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

internal static class DependencyInjection
{
    public static IServiceCollection AddCustomerHandlers(this IServiceCollection services)
    {
        services.AddCommandHandler<InitiateRegistrationCommand, Unit, InitiateRegistrationHandler>();
        services.AddCommandHandler<CompleteRegistrationCommand, Unit, CompleteRegistrationHandler>();
        
        return services;
    }
}