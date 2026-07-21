using System.Security.Cryptography;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public sealed class PasswordRandomStringGenerator : IPasswordGenerator
{
    private readonly RandomPasswordOption _option;

    public PasswordRandomStringGenerator(IOptions<RandomPasswordOption> options)
    {
        _option = options.Value;
    }
    public Password Generate()
    {
        var randomString = RandomNumberGenerator.GetHexString(_option.Length);

        var bytes = System.Text.Encoding.UTF8.GetBytes(randomString);

        var base64 = Convert.ToBase64String(bytes);

        var passwordResult = Password.Create(base64);

        if (passwordResult.IsFailure)
            throw new InvalidOperationException(
                $"Code: {passwordResult.Error!.Code}. Message: {passwordResult.Error.Message}");

        return passwordResult.Value!;
    }
}