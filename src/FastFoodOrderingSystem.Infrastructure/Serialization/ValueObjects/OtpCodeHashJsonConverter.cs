using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class OtpCodeHashJsonConverter : SystemTextJsonConverter<OtpCodeHash>
{
    protected override OtpCodeHash? Create(string value)
    {
        var result = OtpCodeHash.Create(value);

        if (result.IsFailure)
            throw new InvalidOperationException("Can not converter OTP code hash from json data.");

        return result.Value;
    }

    protected override string GetValue(OtpCodeHash value)
    {
        return value.Value;
    }
}