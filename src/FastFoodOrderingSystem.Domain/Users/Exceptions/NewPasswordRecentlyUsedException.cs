using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Users.Exceptions;

public class NewPasswordRecentlyUsedException : DomainException
{
    public NewPasswordRecentlyUsedException() : base(
        code: "change_password_hash.recently_used",
        message: "New password must not same as 4 recentest used password")
    {
    }
}