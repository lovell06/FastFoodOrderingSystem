using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

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