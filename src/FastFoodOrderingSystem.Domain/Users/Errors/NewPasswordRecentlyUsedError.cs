using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.Errors;

public sealed record NewPasswordRecentlyUsedError : DomainError
{
    public NewPasswordRecentlyUsedError() : base(
        Code: "change_password_hash.recently_used",
        Message: "New password must not same as 4 recentest used password")
    {
    }
}