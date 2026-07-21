using FastFoodOrderingSystem.Domain.Common.Abstractions;

namespace FastFoodOrderingSystem.Application.Abstractions.Persistence;

public interface IUnitWork
{
    Task BeginAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
    Task<int> SaveEventsAsync(CancellationToken cancellationToken);
}