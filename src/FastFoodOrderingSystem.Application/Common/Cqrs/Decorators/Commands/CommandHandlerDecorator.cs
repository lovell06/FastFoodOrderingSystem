using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;

public abstract class CommandHandlerDecorator<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
{
    protected readonly IHandler<TCommand, TResponse> Handler;

    public CommandHandlerDecorator(IHandler<TCommand, TResponse> handler)
    {
        Handler = handler;
    }
    public abstract Task<Result<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken);
}