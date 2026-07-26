using FastFoodOrderingSystem.Application.Abstractions.Cache;
using FastFoodOrderingSystem.Application.Abstractions.Cache.CacheServices;
using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;
using FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Common.Cqrs;

internal static class HandlerRegistrationExtension
{
    public static IServiceCollection AddCommandHandler<TCommand, TResponse, TCommandHandler>(
        this IServiceCollection services) 
        where TCommandHandler : class, ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        services.AddScoped<TCommandHandler>();

        services.AddScoped(sp =>
        {
            IHandler<TCommand, TResponse> handler = sp.GetRequiredService<TCommandHandler>();

            handler = new TransactionCommandDecorator<TCommand, TResponse>(
                handler: handler,
                unitWork: sp.GetRequiredService<IUnitWork>(),
                logger: sp.GetRequiredService<ILogger<TransactionCommandDecorator<TCommand, TResponse>>>());

            handler = new PerformanceHandlerDecorator<TCommand, TResponse>(
                handler: handler,
                logger: sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<TCommand, TResponse>>>());

            handler = new LoggingHandlerDecorator<TCommand, TResponse>(
                handler: handler,
                logger: sp.GetRequiredService<ILogger<LoggingHandlerDecorator<TCommand, TResponse>>>());

            return handler;
        });

        return services;
    }

    public static IServiceCollection AddQueryHandler<TQuery, TResponse, TQueryHandler>(
        this IServiceCollection services)
        where TQueryHandler : class, IQueryHandler<TQuery, TResponse> 
        where TQuery : IQuery<TResponse>
    {
        services.AddScoped<TQueryHandler>();

        services.AddScoped(sp =>
        {
            IHandler<TQuery, TResponse> handler = sp.GetRequiredService<TQueryHandler>();

            handler = new CachingQueryDecorator<TQuery, TResponse>(
                handler: handler,
                cacheStore: sp.GetRequiredService<ICacheStore<TResponse>>(),
                logger: sp.GetRequiredService<ILogger<CachingQueryDecorator<TQuery, TResponse>>>(),
                policy: sp.GetRequiredService<ICachePolicy<TQuery>>());

            handler = new PerformanceHandlerDecorator<TQuery, TResponse>(
                handler: handler,
                logger: sp.GetRequiredService<ILogger<PerformanceHandlerDecorator<TQuery, TResponse>>>());

            handler = new LoggingHandlerDecorator<TQuery, TResponse>(
                handler: handler,
                logger: sp.GetRequiredService<ILogger<LoggingHandlerDecorator<TQuery, TResponse>>>());

            return handler;
        });

        return services;
    }
}