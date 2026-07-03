using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;
using FluentAssertions;

namespace FastFoodOrderingSystem.Domain.Tests.Common.ValueObjects;

public sealed class EmailTests
{
    [Fact]
    public void Create_Should_ReturnEmail_When_IsValid()
    {
        const string value = "lovell06@gmail.com";

        var email = Email.Create(value);

        email.Should().NotBeNull();
        email.Value.Should().Be(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abc")]
    [InlineData("abc@")]
    [InlineData("abc.@")]
    [InlineData("abc@gmail")]
    [InlineData("abc@gmail.")]
    [InlineData("abc@.com")]
    [InlineData("abc.com")]
    [InlineData("123456@gmail.com")]
    [InlineData("!*#?:.;,~|}@gmail.com")]
    [InlineData("asb!*#&@gmail.com")]
    [InlineData("abc.@gmail.com")]
    [InlineData(".abc@extension")]
    public void Create_Should_ThrowInvalidEmailException_When_IsInvalid(string value)
    {
        Action act = () => Email.Create(value);

        act.Should().Throw<InvalidEmailException>();
    }
}