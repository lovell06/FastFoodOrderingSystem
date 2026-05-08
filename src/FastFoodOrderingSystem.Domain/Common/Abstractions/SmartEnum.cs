namespace FastFoodOrderingSystem.Domain.Common.Abstractions;

public abstract class SmartEnum<TEnum> where TEnum : SmartEnum<TEnum>
{
    private readonly int _id;
    private readonly string _code;

    protected SmartEnum(int id, string code)
    {
        _id = id;
        _code = code;
    }
}