using System.Diagnostics;
using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;

public sealed class PerformanceHandlerDecorator<TRequest, TResponse> 
    : HandlerDecorator<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<PerformanceHandlerDecorator<TRequest, TResponse>> _logger;
    public PerformanceHandlerDecorator(IHandler<TRequest, TResponse> handler, ILogger<PerformanceHandlerDecorator<TRequest, TResponse>> logger) : base(handler)
    {
        _logger = logger;
    }
    public override async Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        var sw = new Stopwatch();
        try
        {
            sw.Start();

            var result = await Handler.HandleAsync(request, cancellationToken);

            return result;
        }
        finally
        {
            sw.Stop();
            _logger.LogInformation($"{typeof(TRequest).Name} execute in {sw.ElapsedMilliseconds}ms.");
        }
    }
}