using System.Security.Cryptography;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public class OtpService
{    
    public OtpCode GenerateCode(int length)
    {
        int value = RandomNumberGenerator.GetInt32(0, (int)Math.Pow(10, length));
        string code = value.ToString($"D{length}");

        return OtpCode.Create(code);
    }
}