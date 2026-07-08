using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache;

public interface IPendingRegistrationStore
{
    public Task<bool> SaveAsync(
        PendingRegistration pendingRegistration,
        IDateTimeProvider clock,
        CancellationToken cancellationToken = default);

    public Task<PendingRegistration?> GetByEmailAsync(
        Email email,
        CancellationToken cancellationToken = default);

    public Task<bool> RemoveAsync(
        Email email,
        CancellationToken cancellationToken = default);
}