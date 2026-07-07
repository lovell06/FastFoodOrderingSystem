using FastFoodOrderingSystem.Application.Common.Results;

namespace FastFoodOrderingSystem.Application.Features.Auth.Login;

public class LoginError
{
    public static readonly Error Failed = Error.Failure("login_error.failed", "Email or password incorrect.");
}