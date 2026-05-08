using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

public sealed class InvalidAddressException : DomainException
{
    private InvalidAddressException(string code, string message) : base(code, message)
    {
    }

    public static InvalidAddressException ProvinceOrCityEmpty()
    {
        return new InvalidAddressException(
            code: "address.province_or_city_empty",
            message: "Province or City must not be empty.");
    }

    public static InvalidAddressException WardOrCommuneEmpty()
    {
        return new InvalidAddressException(
            code: "address.ward_or_commune_empty",
            message: "Ward or Commune must not be empty.");
    }

    public static InvalidAddressException DetailEmpty()
    {
        return new InvalidAddressException(
            code: "address.detail_empty",
            message: "Detail addres must not be empty");
    }

    public static InvalidAddressException ExceedsMaxLength(int maxLength)
    {
        return new InvalidAddressException(
            code: "address.exceeds_max_length", 
            message: $"Address must not exceed {maxLength} characters");
    }
}