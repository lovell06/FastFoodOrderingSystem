using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

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

    public static Address Create(string provinceCity, string wardCommune, string detail)
    {
        provinceCity = provinceCity.Trim();
        wardCommune = wardCommune.Trim();
        detail = detail.Trim();

        Validate(provinceCity, wardCommune, detail);
        
        return new Address(provinceCity, wardCommune, detail);
    }

    private static void Validate(string provinceCity, string wardCommune, string detail)
    {
        if (string.IsNullOrWhiteSpace(provinceCity))
            throw InvalidAddressException.ProvinceOrCityEmpty();

        if (string.IsNullOrWhiteSpace(wardCommune))
            throw InvalidAddressException.WardOrCommuneEmpty();

        if (string.IsNullOrWhiteSpace(detail))
            throw InvalidAddressException.DetailEmpty();

        if (provinceCity.Length + wardCommune.Length + detail.Length > MaxLength)
            throw InvalidAddressException.ExceedsMaxLength(MaxLength);
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
            throw new FormatException("Invalid address format.");

        return Create(
            detail: parts[0],
            wardCommune: parts[1],
            provinceCity: parts[2]);
    }
}