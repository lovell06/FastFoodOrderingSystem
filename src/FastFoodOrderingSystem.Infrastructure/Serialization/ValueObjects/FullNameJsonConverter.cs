using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class FullNameJsonConverter : SystemTextJsonConverter<FullName>
{
    protected override FullName? Create(string value)
    {
        return FullName.Create(value);
    }

    protected override string GetValue(FullName value)
    {
        return value.Value;
    }
}