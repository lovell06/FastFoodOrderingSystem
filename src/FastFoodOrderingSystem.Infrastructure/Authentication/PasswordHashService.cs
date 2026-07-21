using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Domain.Users.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public class PasswordHashService : IPasswordHashService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordHashService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public PasswordHash Hash(User user, Password password)
    {
        var raw = _passwordHasher.HashPassword(user, password.Value);

        var result = PasswordHash.Create(raw);

        if (result.IsFailure)
            throw new InvalidOperationException($"Hash password failed.");

        return result.Value!;
    }

    public bool Verify(User user, Password providedPassword, PasswordHash hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user: user, 
            hashedPassword: hashedPassword.Value,
            providedPassword: providedPassword.Value);

        return result == PasswordVerificationResult.Success || 
               result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}