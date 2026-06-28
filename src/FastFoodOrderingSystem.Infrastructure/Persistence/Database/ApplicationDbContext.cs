using FastFoodOrderingSystem.Domain.PendingRegistrations;
using FastFoodOrderingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Database;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; }
    public DbSet<PendingRegistration> PendingRegistrations { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}
