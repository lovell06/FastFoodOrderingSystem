using FastFoodOrderingSystem.Domain.Common.Abstractions;

namespace FastFoodOrderingSystem.Domain.Common.Enums;

public sealed class UserRole : SmartEnum<UserRole>
{
    public static readonly UserRole Admin = new UserRole("admin");
    public static readonly UserRole Employee = new UserRole("employee");
    public static readonly UserRole Customer = new UserRole("customer");

    private UserRole(string roleCode) : base(roleCode)
    {
    }

}