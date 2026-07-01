namespace FastFoodOrderingSystem.Application.Abstractions.Persistence;

public interface IUnitWork
{
    Task<int> CommitAsync();
}