using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;

public sealed class PerformanceHandlerDecorator<TRequest, TResult> : HandlerDecorator<TRequest, TResult>
{
    private readonly ILogger<PerformanceHandlerDecorator<TRequest, TResult>> _logger;
    public PerformanceHandlerDecorator(IHandler<TRequest, TResult> handler, ILogger<PerformanceHandlerDecorator<TRequest, TResult>> logger) : base(handler)
    {
        _logger = logger;
    }
    public override async Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken)
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