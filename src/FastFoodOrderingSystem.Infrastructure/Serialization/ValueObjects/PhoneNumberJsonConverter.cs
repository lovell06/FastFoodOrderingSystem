using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class PhoneNumberJsonConverter : SystemTextJsonConverter<PhoneNumber>
{
    protected override PhoneNumber? Create(string value)
    {
        return PhoneNumber.Create(value);
    }

    protected override string GetValue(PhoneNumber value)
    {
        return value.Value;
    }
}