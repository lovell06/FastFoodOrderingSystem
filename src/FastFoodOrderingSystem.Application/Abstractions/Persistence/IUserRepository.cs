using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;

namespace FastFoodOrderingSystem.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<IReadOnlyCollection<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<bool> EmailAlreadyExistedAsync(Email email);
    Task<IReadOnlyCollection<UserShippingAddress>> GetShippingAddressesAsync(Guid id);
    Task<UserShippingAddress?> GetDefaultShippingAddressAsync(Guid id);
}