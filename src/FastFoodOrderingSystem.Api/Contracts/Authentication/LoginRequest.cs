using FastFoodOrderingSystem.Application.Features.Auth.Login;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record LoginRequest(string Email, string Password)
{
    public LoginCommand ToCommand()
    {
        return new LoginCommand(Email, Password);
    }
}