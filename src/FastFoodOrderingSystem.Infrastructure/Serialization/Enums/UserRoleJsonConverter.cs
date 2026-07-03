using FastFoodOrderingSystem.Domain.Common.Enums;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.Enums;

public class UserRoleJsonConverter : SystemTextJsonConverter<UserRole>
{
    protected override UserRole? Create(string value)
    {
        return UserRole.FromCode(value);
    }

    protected override string GetValue(UserRole value)
    {
        return value.Code;
    }
}