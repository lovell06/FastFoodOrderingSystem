using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Domain.Users;

public class UserPasswordHistory : Entity<long>
{
    public const int MaxStoredPasswordCount = 4;
    public PasswordHash PasswordHash { get; private set; }
    public DateTime ChangedAt { get; }

    protected UserPasswordHistory()
    {
    }

    private UserPasswordHistory(PasswordHash passwordHash, DateTime changedAt)
    {
        PasswordHash = passwordHash;
        ChangedAt = changedAt;
    }

    public static UserPasswordHistory Create(PasswordHash passwordHash, DateTime changedAt)
    {
        return new UserPasswordHistory(passwordHash, changedAt);
    }
}