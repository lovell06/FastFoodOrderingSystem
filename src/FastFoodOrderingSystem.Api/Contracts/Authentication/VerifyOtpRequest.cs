using FastFoodOrderingSystem.Application.Features.Auth.Register;

namespace FastFoodOrderingSystem.Api.Contracts.Authentication;

public record VerifyOtpRequest(string Email, string OtpCode)
{
    public VerifyRegisterCommand ToCommand()
    {
        return new(Email: Email, Code: OtpCode);
    }
}