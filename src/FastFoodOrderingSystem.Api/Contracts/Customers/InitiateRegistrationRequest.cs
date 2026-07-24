using FastFoodOrderingSystem.Application.Features.Customers.InitiateRegistration;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public sealed record InitiateRegistrationRequest(
    string FullName,
    string Email,
    string Password,
    string PhoneNumber)
{
    public InitiateRegistrationCommand ToCommand()
    {
        return new InitiateRegistrationCommand(FullName, Email, Password, PhoneNumber);
    }
}