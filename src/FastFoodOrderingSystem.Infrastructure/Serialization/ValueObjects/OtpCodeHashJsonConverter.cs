using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class OtpCodeHashJsonConverter : SystemTextJsonConverter<OtpCodeHash>
{
    protected override OtpCodeHash? Create(string value)
    {
        return OtpCodeHash.Create(value);
    }

    protected override string GetValue(OtpCodeHash value)
    {
        return value.Value;
    }
}