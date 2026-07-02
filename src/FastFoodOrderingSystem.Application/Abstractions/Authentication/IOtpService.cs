using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Application.Abstractions.Authentication;

public interface IOtpService
{
    OtpCode GenerateCode(int length);
}