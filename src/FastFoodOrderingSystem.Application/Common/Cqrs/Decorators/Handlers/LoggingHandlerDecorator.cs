using FastFoodOrderingSystem.Application.Common.Results;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Handlers;

public class LoggingHandlerDecorator<TRequest, TResponse> 
    : HandlerDecorator<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingHandlerDecorator<TRequest, TResponse>> _logger;
    public LoggingHandlerDecorator(
        IHandler<TRequest, TResponse> handler, ILogger<LoggingHandlerDecorator<TRequest, TResponse>> logger) : base(handler)
    {
        _logger = logger;
    }

    public override async Task<Result<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken)
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