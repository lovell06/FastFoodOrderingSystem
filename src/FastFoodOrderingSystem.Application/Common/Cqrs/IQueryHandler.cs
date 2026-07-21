namespace FastFoodOrderingSystem.Application.Common.Cqrs;

public interface IQueryHandler<TQuery, TResult> : IHandler<TQuery, TResult> where TQuery : IQuery;