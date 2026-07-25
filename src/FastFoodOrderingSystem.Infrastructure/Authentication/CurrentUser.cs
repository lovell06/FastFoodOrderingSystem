using System.Security.Claims;
using FastFoodOrderingSystem.Application.Abstractions.Authentication;
using FastFoodOrderingSystem.Domain.Common.Enums;
using Microsoft.AspNetCore.Http;

namespace FastFoodOrderingSystem.Infrastructure.Authentication;

public sealed class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;
    
    public Guid Id
    {
        get
        {
            var value = _user?.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(value, out var id)
                ? id
                : throw new InvalidOperationException("Current user is not authenticated.");
        }
    }

    public UserRole Role
    {
        get
        {
            var value = _user?.FindFirstValue(ClaimTypes.Role);

            return value is null 
                ? throw new InvalidOperationException("Current user is not authenticated.") 
                : UserRole.FromCode(value);
        }
    }
}