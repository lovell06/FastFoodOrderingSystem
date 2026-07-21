using System.Security.Cryptography;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public class OtpService : IOtpService
{    
    public OtpCode GenerateCode(int length)
    {
        int value = RandomNumberGenerator.GetInt32(0, (int)Math.Pow(10, length));
        string code = value.ToString($"D{length}");

        var result = OtpCode.Create(code);

        if (result.IsFailure)
            throw new InvalidOperationException($"Generate OTP code failed.");

        return result.Value!;
    }
}