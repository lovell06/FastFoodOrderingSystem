using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Abstractions.Cache;

public interface IPendingRegistrationStore
{
    public Task<bool> SaveAsync(
        PendingRegistration pendingRegistration,
        CancellationToken cancellationToken = default);

    public Task<PendingRegistration?> GetAsync(
        Email email,
        CancellationToken cancellationToken = default);

    public Task<bool> RemoveAsync(
        Email email,
        CancellationToken cancellationToken = default);
}