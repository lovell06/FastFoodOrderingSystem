using FastFoodOrderingSystem.Application.Features.Customers.CompleteRegistration;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record CompleteRegistrationRequest(string Email, string OtpCode)
{
    public CompleteRegistrationCommand ToCommand()
    {
        return new(Email: Email, Code: OtpCode);
    }
}