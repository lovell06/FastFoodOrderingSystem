using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Infrastructure.Eventing.EventMappers;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Repositories;

public sealed class UnitWork : IUnitWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;
    private readonly ILogger<UnitWork> _logger;
    public UnitWork(ApplicationDbContext context, ILogger<UnitWork> logger)
    {
        _context = context;
        _logger = logger;
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

        var count = await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation($"Changed {count} rows.");
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

    public IReadOnlyCollection<IDomainEvent> DequeueDomainEvents()
    {
        var entities = _context.ChangeTracker
            .Entries<IHasDomainEvent>()
            .Select(entry => entry.Entity)
            .ToList();

        var events = entities
            .SelectMany(e => e.DomainEvents)
            .ToList()
            .AsReadOnly();

        foreach (var e in entities)
        {
            e.ClearDomainEvent();
        }
        
        return events;
    }

    public async Task StoreEventsAsync(CancellationToken cancellationToken)
    {
        var domainEvents = DequeueDomainEvents();

        if (domainEvents.Count == 0)
            return;

        var outboxMessages = domainEvents
            .Select(e => OutboxMessage.Create(e.ToIntegration()))
            .ToList();
        
        await _context.OutboxMessages.AddRangeAsync(outboxMessages, cancellationToken);
        
        _logger.LogInformation($"Events was been store. {outboxMessages.Count} events.");
    }
}