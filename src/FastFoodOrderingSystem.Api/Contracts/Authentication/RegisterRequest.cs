using FastFoodOrderingSystem.Application.Features.Customers.Register;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public sealed record RegisterRequest(
    string FullName,
    string Email,
    string Password,
    string PhoneNumber)
{
    public RegisterCommand ToCommand()
    {
        return new RegisterCommand(FullName, Email, Password, PhoneNumber);
    }
}