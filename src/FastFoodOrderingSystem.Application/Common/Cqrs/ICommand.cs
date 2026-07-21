namespace FastFoodOrderingSystem.Application.Common.Cqrs;

public interface ICommand<TResponse> : IRequest<TResponse>;