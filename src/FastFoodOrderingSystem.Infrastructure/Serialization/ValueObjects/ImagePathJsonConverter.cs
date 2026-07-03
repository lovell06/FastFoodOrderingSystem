using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class ImagePathJsonConverter : SystemTextJsonConverter<ImagePath>
{
    protected override ImagePath? Create(string value)
    {
        return ImagePath.Create(value);
    }

    protected override string GetValue(ImagePath value)
    {
        return value.Value;
    }
}