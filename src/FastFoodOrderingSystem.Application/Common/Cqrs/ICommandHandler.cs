namespace FastFoodOrderingSystem.Application.Common.Cqrs;

public interface ICommandHandler<TCommand, TResult> : IHandler<TCommand, TResult> 
    where TCommand : ICommand;