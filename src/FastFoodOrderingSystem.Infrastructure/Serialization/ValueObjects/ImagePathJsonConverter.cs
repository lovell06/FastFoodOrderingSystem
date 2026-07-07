using FastFoodOrderingSystem.Domain.Common.ValueObjects;

namespace FastFoodOrderingSystem.Infrastructure.Serialization.ValueObjects;

public class ImagePathJsonConverter : SystemTextJsonConverter<ImagePath>
{
    protected override ImagePath? Create(string value)
    {
        var result = ImagePath.Create(value);

        if (result.IsFailure)
            throw new InvalidOperationException("Can not converter image path from json data.");

        return result.Value;
    }

    protected override string GetValue(ImagePath value)
    {
        return value.Value;
    }
}