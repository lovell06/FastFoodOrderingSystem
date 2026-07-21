namespace FastFoodOrderingSystem.Application.Common.Cqrs;

public interface ICommandHandler<in TCommand, TResponse> 
    : IHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>;