using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class EmailJsonConverter : SystemTextJsonConverter<Email>
{
    protected override Email? Create(string value)
    {
        return Email.Create(value);
    }

    protected override string GetValue(Email value)
    {
        return value.Value;
    }
}