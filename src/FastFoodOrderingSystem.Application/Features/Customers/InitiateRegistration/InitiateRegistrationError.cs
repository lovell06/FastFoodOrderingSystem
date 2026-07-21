using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Application.Features.Customers.InitiateRegistration;

public static class InitiateRegistrationError
{
    public static Error EmailAlreadyExisted(Email email)
    {
        return Error.Conflict(
            "register_error.email_already_existed",
            $"Account with email {email.Value} existed.");
    }
}