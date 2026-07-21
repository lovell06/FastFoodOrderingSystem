namespace FastFoodOrderingSystem.Application.Common.Cqrs;

public interface IQueryHandler<in TQuery, TResult> 
    : IHandler<TQuery, TResult> where TQuery : IQuery<TResult>;