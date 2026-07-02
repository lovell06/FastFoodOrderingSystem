using FastFoodOrderingSystem.Application.Common.Results;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

namespace FastFoodOrderingSystem.Application.Features.Auth.Register;

public class RegisterError
{
    public static Error EmailAlreadyExisted(Email email)
    {
        return Error.Conflict(
            "register_error.email_already_existed",
            $"Account with email {email} existed.");
    }

    public static Error InvalidEmail(InvalidEmailException exception)
    {
        return Error.Validation(
            exception.Code,
            exception.Message);
    }

    public static Error InvalidFullName(InvalidFullNameException exception)
    {
        return Error.Validation(
            exception.Code,
            exception.Message);
    }

    public static Error InvalidPassword(InvalidPasswordException exception)
    {
        return Error.Validation(
            exception.Code,
            exception.Message);
    }

    public static Error InvalidPhoneNumber(InvalidPhoneNumberException exception)
    {
        return Error.Validation(
            exception.Code,
            exception.Message);
    }

    public static Error InvalidOtpCode(InvalidOtpCodeException exception)
    {
        return Error.Validation(
            exception.Code,
            exception.Message);
    }
}