using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IOtpService
{
    OtpCode GenerateCode(int length);
}