using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public sealed record Address
{
    public const int MaxLength = 300;
    public string ProvinceCity { get; }
    public string WardCommune { get; }
    public string Detail { get; }

    private Address(string provinceCity, string wardCommune, string detail)
    {
        ProvinceCity = provinceCity;
        WardCommune = wardCommune;
        Detail = detail;
    }

    public static Address Create(string provinceCity, string wardCommune, string detail)
    {
        provinceCity = provinceCity.Trim();
        wardCommune = wardCommune.Trim();
        detail = detail.Trim();

        if (string.IsNullOrWhiteSpace(provinceCity))
            throw InvalidAddressException.ProvinceOrCityEmpty();

        if (string.IsNullOrWhiteSpace(wardCommune))
            throw InvalidAddressException.WardOrCommuneEmpty();

        if (string.IsNullOrWhiteSpace(detail))
            throw InvalidAddressException.DetailEmpty();

        if (provinceCity.Length + wardCommune.Length + detail.Length > MaxLength)
            throw InvalidAddressException.ExceedsMaxLength(MaxLength);
        
        return new Address(provinceCity, wardCommune, detail);
    }

    public override string ToString()
    {
        return $"{ProvinceCity}, {WardCommune}, {Detail}";
    }
}