using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public static class InvalidAddressError
{
    public static DomainError ProvinceOrCityEmpty()
    {
        return new(
            Code: "invalid_address_error.province_or_city_empty",
            Message: "Province or City must not be empty.");
    }

    public static DomainError WardOrCommuneEmpty()
    {
        return new(
            Code: "invalid_address_error.ward_or_commune_empty",
            Message: "Ward or Commune must not be empty.");
    }

    public static DomainError DetailEmpty()
    {
        return new(
            Code: "invalid_address_error.detail_empty",
            Message: "Detail address must not be empty");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "invalid_address_error.exceeds_max_length",
            Message: $"Address must not exceed {maxLength} characters");
    }
    public static DomainError InvalidFormat()
    {
        return new(
            Code: "invalid_address_error.invalid_format",
            Message: $"Address invalid format.");
    }
}