using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    Task<bool> EmailAlreadyExistedAsync(Email email, CancellationToken cancellationToken);
    Task<User?> GetWithShippingAddressesAsync(Guid id, CancellationToken cancellationToken);
    Task InsertAsync(User user, CancellationToken cancellationToken);
}