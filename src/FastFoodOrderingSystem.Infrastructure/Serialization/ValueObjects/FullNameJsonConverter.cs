using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class FullNameJsonConverter : SystemTextJsonConverter<FullName>
{
    protected override FullName? Create(string value)
    {
        var result = FullName.Create(value);

        if (result.IsFailure)
            throw new InvalidOperationException("Can not converter full name from json data.");

        return result.Value;
    }

    protected override string GetValue(FullName value)
    {
        return value.Value;
    }
}