using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class AddressJsonConverter : SystemTextJsonConverter<Address>
{
    protected override Address? Create(string value)
    {
        return Address.ParseFromDatabase(value);
    }

    protected override string GetValue(Address value)
    {
        return value.ToDatabaseString();
    }
}