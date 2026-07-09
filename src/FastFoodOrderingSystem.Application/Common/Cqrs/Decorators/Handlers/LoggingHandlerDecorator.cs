using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Common.Handlers.HandlerDecorators;

public class LoggingHandlerDecorator<TRequest, TResult> : HandlerDecorator<TRequest, TResult>
{
    private readonly ILogger<IHandler<TRequest, TResult>> _logger;
    public LoggingHandlerDecorator(
        IHandler<TRequest, TResult> handler, ILogger<IHandler<TRequest, TResult>> logger) : base(handler)
    {
        _logger = logger;
    }

    public override async Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Handling {typeof(TRequest).Name}...");

        try
        {
            return await Handler.HandleAsync(request, cancellationToken);
        }
        finally
        {
            _logger.LogInformation($"Handled {typeof(TRequest).Name}.");
        }

    }
}