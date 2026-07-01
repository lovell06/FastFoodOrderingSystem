using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyCollection<User>> GetAllAsync()
    {
        var users = await _context.Users
            .ToListAsync();

        return users.AsReadOnly();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await _context.Users
            .SingleAsync(u => u.Id == id);

        return user;
    }

    public async Task Insert(User user)
    {
        await _context.AddAsync(user);
    }

    public async Task<bool> EmailAlreadyExistedAsync(Email email)
    {
        var result = await _context.Users
            .AnyAsync(u => u.Email == email);

        return result;
    }

    public async Task<User?> GetWithShippingAddressesAsync(Guid id)
    {
        var user = await _context.Users
            .Include(u => u.ShippingAddresses)
            .SingleAsync(u => u.Id == id);

        return user;
    }
}