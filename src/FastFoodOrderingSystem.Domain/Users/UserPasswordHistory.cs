using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Domain.Users;

public class UserPasswordHistory : Entity<long>
{
    public const int MaxStoredPasswordCount = 4;
    public PasswordHash PasswordHash { get; private set; }
    public DateTime ChangeAt { get; }

    private UserPasswordHistory(PasswordHash passwordHash, DateTime changeAt)
    {
        PasswordHash = passwordHash;
        ChangeAt = changeAt;
    }

    public static UserPasswordHistory Create(PasswordHash passwordHash, DateTime changeAt)
    {
        return new UserPasswordHistory(passwordHash, changeAt);
    }
}