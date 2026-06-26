using FastFoodOrderingSystem.Domain.Common.Abstractions;

namespace FastFoodOrderingSystem.Domain.Common.Enums;

public sealed class UserRole : SmartEnum<UserRole>
{
    public static readonly UserRole Admin = new UserRole(0, "admin");
    public static readonly UserRole Employee = new UserRole(1, "employee");
    public static readonly UserRole Customer = new UserRole(2, "customer");

    private UserRole(int id, string roleCode) : base(id, roleCode)
    {
    }

}