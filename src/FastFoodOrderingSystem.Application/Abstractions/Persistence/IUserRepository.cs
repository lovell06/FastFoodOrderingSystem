using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<bool> EmailAlreadyExistedAsync(Email email);
    Task<User?> GetWithShippingAddressesAsync(Guid id);
    Task InsertAsync(User user);
}