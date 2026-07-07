using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class PhoneNumberJsonConverter : SystemTextJsonConverter<PhoneNumber>
{
    protected override PhoneNumber? Create(string value)
    {
        var result = PhoneNumber.Create(value);

        if (result.IsFailure)
            throw new InvalidOperationException("Can not converter phone number from json data.");

        return result.Value;
    }

    protected override string GetValue(PhoneNumber value)
    {
        return value.Value;
    }
}