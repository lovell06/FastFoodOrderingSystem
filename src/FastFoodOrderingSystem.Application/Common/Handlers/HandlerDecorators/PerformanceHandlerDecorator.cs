using System.Diagnostics;

namespace FastFoodOrderingSystem.Application.Common.Handlers.HandlerDecorators;

public sealed class PerformanceHandlerDecorator<TRequest, TResult> : HandlerDecorator<TRequest, TResult>
{
    public PerformanceHandlerDecorator(IHandler<TRequest, TResult> handler) : base(handler)
    {
    }
    public override async Task<TResult> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        var sw = new Stopwatch();
        try
        {
            sw.Start();

            var result = await Handler.HandleAsync(request, cancellationToken);

            sw.Stop();

            return result;
        }
        catch (Exception)
        {
            sw.Stop();
            throw;
        }
    }
}