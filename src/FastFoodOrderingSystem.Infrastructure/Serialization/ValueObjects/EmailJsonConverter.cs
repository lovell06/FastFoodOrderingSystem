using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class EmailJsonConverter : SystemTextJsonConverter<Email>
{
    protected override Email? Create(string value)
    {
        var result =  Email.Create(value);

        if (result.IsFailure)
            throw new InvalidOperationException("Can not converter email from json data.");

        return result.Value;
    }

    protected override string GetValue(Email value)
    {
        return value.Value;
    }
}