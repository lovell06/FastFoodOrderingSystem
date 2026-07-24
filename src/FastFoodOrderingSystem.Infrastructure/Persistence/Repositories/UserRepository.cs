using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
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

    public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .ToListAsync(cancellationToken);

        return users.AsReadOnly();
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .SingleAsync(u => u.Id == id, cancellationToken);
    }

    public async Task InsertAsync(User user, CancellationToken cancellationToken)
    {
        await _context.AddAsync(user, cancellationToken);
    }

    public async Task<bool> EmailAlreadyExistedAsync(Email email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetWithShippingAddressesAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.ShippingAddresses)
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetWithPasswordHistoriesAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.PasswordHistories
                .OrderByDescending(h => h.ChangedAt))
            .SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetWithPasswordHistoriesByEmailAsync(Email email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(u => u.PasswordHistories
                .OrderByDescending(h => h.ChangedAt))
            .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}