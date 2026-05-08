using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Users.Exceptions;

public class NewPasswordSameAsCurrentPasswordException : DomainException
{
    public NewPasswordSameAsCurrentPasswordException() : base(
        code: "change_password_hash.new_password_same_as_current_password",
        message: "New password must not same as current password.")
    {
    }
}