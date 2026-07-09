using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Application.Abstractions.Time;
using FastFoodOrderingSystem.Domain.Users;
using FastFoodOrderingSystem.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public sealed class JwtProvider : IAccessTokenProvider
{
    private readonly JwtOption _option;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtProvider(IOptions<JwtOption> option, IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
        _option = option.Value;
    }

    public string Generate(User user)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_option.Key));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
            new(ClaimTypes.Role, user.Role.Code)
        };

        var token = new JwtSecurityToken(
            issuer: _option.Issuer,
            audience: _option.Audience,
            claims: claims,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_option.ExpireMinutes),
            signingCredentials: credential);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}