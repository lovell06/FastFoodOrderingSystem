using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.Errors;

public sealed record NewPasswordSameAsCurrentPasswordError : DomainError
{
    public NewPasswordSameAsCurrentPasswordError() : base(
        Code: "change_password_hash.new_password_same_as_current_password",
        Message: "New password must not same as current password.")
    {
    }
}