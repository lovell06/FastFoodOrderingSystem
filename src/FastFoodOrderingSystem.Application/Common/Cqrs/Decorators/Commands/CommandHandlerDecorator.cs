namespace FastFoodOrderingSystem.Application.Common.Cqrs.Decorators.Commands;

public abstract class CommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
{
    protected readonly IHandler<TCommand, TResult> Handler;

    public CommandHandlerDecorator(IHandler<TCommand, TResult> handler)
    {
        Handler = handler;
    }
    public abstract Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}