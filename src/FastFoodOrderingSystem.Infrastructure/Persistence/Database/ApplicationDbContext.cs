using FastFoodOrderingSystem.Domain.PendingRegistrations;
using FastFoodOrderingSystem.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace FastFoodOrderingSystem.Infrastructure.Persistence.Database;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; }
    public DbSet<PendingRegistration> PendingRegistrations { get; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
    }
}
