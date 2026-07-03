using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class PasswordHashJsonConverter : SystemTextJsonConverter<PasswordHash>
{
    protected override PasswordHash? Create(string value)
    {
        return PasswordHash.Create(value);
    }

    protected override string GetValue(PasswordHash value)
    {
        return value.Value;
    }
}