using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;
using FluentAssertions;

namespace FastFoodOrderingSystem.Domain.Tests.Common.ValueObjects;

public sealed class AddressTests
{
    [Fact]
    public void Create_And_LoadFromDatabase_Should_Match_When_IsValid()
    {
        const string provinceCity = "Ho Chi Minh";
        const string wardCommune = "An Hoi Tay";
        const string detail = "xyz/xyz Duong xyz";

        var address = Address.Create(provinceCity, wardCommune, detail);
        var addressFromDb = Address.ParseFromDatabase($"{detail}|{wardCommune}|{provinceCity}");

        address.Should().Be(addressFromDb);
    }
    [Fact]
    public void Create_Should_Return_String_When_IsValid()
    {
        const string provinceCity = "Ho Chi Minh";
        const string wardCommune = "An Hoi Tay";
        const string detail = "xyz/xyz Duong xyz";

        var address = Address.Create(
            provinceCity: provinceCity, 
            wardCommune: wardCommune, 
            detail: detail);

        address.ToString().Should().Be($"{detail}, {wardCommune}, {provinceCity}");
    }

    [Fact]
    public void Create_Should_ReturnPatternSaveDatabase_When_IsValid()
    {
        const string provinceCity = "Ho Chi Minh";
        const string wardCommune = "An Hoi Tay";
        const string detail = "xyz/xyz Duong xyz";

        var address = Address.Create(
            provinceCity: provinceCity,
            wardCommune: wardCommune,
            detail: detail);

        address.ToDatabaseString().Should().Be($"{detail}|{wardCommune}|{provinceCity}");
    }

    [Theory]
    [InlineData("", "An Hoi Tay", "Duong xxx")]
    [InlineData("Ho Chi Minh", "", "Duong xxx")]
    [InlineData("Ho Chi Minh", "An Hoi Tay", "")]
    [InlineData("   ", "   ", "   ")]
    public void Create_Should_ThrowInvalidAddressException_When_IsInvalid(
        string provinceCity, 
        string wardCommune, 
        string detail)
    {
        Action act = () => Address.Create(
            provinceCity: provinceCity,
            wardCommune: wardCommune,
            detail: detail);

        act.Should().Throw<InvalidAddressException>();
    }
}