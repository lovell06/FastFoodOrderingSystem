using FastFoodOrderingSystem.Application.Features.Customers.Register;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record VerifyRegisterRequest(string Email, string OtpCode)
{
    public VerifyRegisterCommand ToCommand()
    {
        return new(Email: Email, Code: OtpCode);
    }
}