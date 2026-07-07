using System.Security.Cryptography;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public class OtpHashService : IOtpHashService
{
    private readonly Byte[] _secretKey;

    public OtpHashService(IOptions<OtpOption> option)
    {
        _secretKey = System.Text.Encoding.UTF8.GetBytes(option.Value.SecretKey);
    }

    public OtpCodeHash Hash(OtpCode code)
    {
        using var hmac = new HMACSHA256(_secretKey);

        var hashCode = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(code.Value));

        var result = OtpCodeHash.Create(Convert.ToHexString(hashCode));

        if (result.IsFailure)
            throw new InvalidOperationException("Hash OTP code failed.");

        return result.Value!;
    }

    public bool Verify(OtpCode providedCode, OtpCodeHash hashedCode)
    {
        return Hash(providedCode) == hashedCode;
    }
}