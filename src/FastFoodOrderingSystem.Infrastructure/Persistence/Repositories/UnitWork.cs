using FastFoodOrderingSystem.Application.Abstractions.Persistence;
using FastFoodOrderingSystem.Infrastructure.Persistence.Database;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Repositories;

public class UnitWork : IUnitWork
{
    private readonly ApplicationDbContext _context;
    public UnitWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }
}