namespace FastFoodOrderingSystem.Domain.Common.Abstractions;

public abstract class SmartEnum<TEnum> where TEnum : SmartEnum<TEnum>
{
    public const int MaxLengthCode = 20;
    public readonly string Code;

    protected SmartEnum(string code)
    {
        Code = code;
    }

    private static IReadOnlyCollection<TEnum> GetAll()
    {
        return typeof(TEnum)
            .GetFields(System.Reflection.BindingFlags.Public |
                       System.Reflection.BindingFlags.Static |
                       System.Reflection.BindingFlags.DeclaredOnly)
            .Where(f => f.FieldType == typeof(TEnum))
            .Select(f => (TEnum)f.GetValue(null)!)
            .ToList()
            .AsReadOnly();
    }

    public static TEnum FromCode(string value)
    {
        return GetAll().Single(f => f.Code == value);
    }
}