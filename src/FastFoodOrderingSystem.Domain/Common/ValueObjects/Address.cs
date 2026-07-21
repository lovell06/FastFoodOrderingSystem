using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record Address
{
    public const int MaxLength = 300;
    public string ProvinceCity { get; init; }
    public string WardCommune { get; init; }
    public string Detail { get; }

    private Address(string provinceCity, string wardCommune, string detail)
    {
        ProvinceCity = provinceCity;
        WardCommune = wardCommune;
        Detail = detail;
    }

    public static DomainResult<Address> Create(string provinceCity, string wardCommune, string detail)
    {
        provinceCity = provinceCity.Trim();
        wardCommune = wardCommune.Trim();
        detail = detail.Trim();

        var error = Validate(provinceCity, wardCommune, detail);

        if (error is not null)
            return DomainResult<Address>.Failure(error);
        
        return DomainResult<Address>.Success(new Address(provinceCity, wardCommune, detail));
    }

    private static DomainError? Validate(string provinceCity, string wardCommune, string detail)
    {
        if (string.IsNullOrWhiteSpace(provinceCity))
            return AddressError.ProvinceOrCityEmpty();

        if (string.IsNullOrWhiteSpace(wardCommune))
            return AddressError.WardOrCommuneEmpty();

        if (string.IsNullOrWhiteSpace(detail))
            return AddressError.DetailEmpty();

        if (provinceCity.Length + wardCommune.Length + detail.Length > MaxLength)
            return AddressError.ExceedsMaxLength(MaxLength);

        return null;
    }

    public override string ToString()
    {
        return $"{Detail}, {WardCommune}, {ProvinceCity}";
    }

    public string ToDatabaseString()
    {
        return $"{Detail}|{WardCommune}|{ProvinceCity}";
    }

    public static Address ParseFromDatabase(string value)
    {
        var parts = value.Split("|");

        if (parts.Length != 3)
            throw new InvalidOperationException("Can not parse address from Database.");

        var result = Create(
            detail: parts[0],
            wardCommune: parts[1],
            provinceCity: parts[2]);

        if (result.IsFailure)
            throw new InvalidOperationException("Address from database invalid.");

        return result.Value!;
    }
}