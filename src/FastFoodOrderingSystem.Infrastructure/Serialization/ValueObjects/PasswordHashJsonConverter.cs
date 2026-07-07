using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class PasswordHashJsonConverter : SystemTextJsonConverter<PasswordHash>
{
    protected override PasswordHash? Create(string value)
    {
        var result = PasswordHash.Create(value);

        if (result.IsFailure)
            throw new InvalidOperationException("Can not converter password hash from json data.");

        return result.Value;
    }

    protected override string GetValue(PasswordHash value)
    {
        return value.Value;
    }
}