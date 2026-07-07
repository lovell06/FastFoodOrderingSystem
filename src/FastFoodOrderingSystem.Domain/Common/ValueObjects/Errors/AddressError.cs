using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public sealed class AddressError
{
    public static DomainError ProvinceOrCityEmpty()
    {
        return new(
            Code: "address.province_or_city_empty",
            Message: "Province or City must not be empty.");
    }

    public static DomainError WardOrCommuneEmpty()
    {
        return new(
            Code: "address.ward_or_commune_empty",
            Message: "Ward or Commune must not be empty.");
    }

    public static DomainError DetailEmpty()
    {
        return new(
            Code: "address.detail_empty",
            Message: "Detail addres must not be empty");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "address.exceeds_max_length",
            Message: $"Address must not exceed {maxLength} characters");
    }
    public static DomainError InvalidFormat()
    {
        return new(
            Code: "address.invalid_format",
            Message: $"Address invalid format.");
    }
}