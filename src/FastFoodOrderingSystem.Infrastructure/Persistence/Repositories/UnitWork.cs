using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Repositories;

public class UnitWork : IUnitWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;
    public UnitWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task BeginAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
            throw new InvalidOperationException(
                "An Transaction has already been started.");

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException(
                "No active transaction.");

        await _context.SaveChangesAsync(cancellationToken);

        await _transaction.CommitAsync(cancellationToken);

        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task RollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is null)
            throw new InvalidOperationException(
                "No active transaction.");

        await _transaction.RollbackAsync(cancellationToken);

        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}