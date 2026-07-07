using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

public sealed class ImagePathError
{
    public static DomainError Empty()
    {
        return new(
            Code: "image_path.empty",
            Message: "Image path must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "image_path.exceeds_max_length",
            Message: $"Image path must not exceed {maxLength} characters.");
    }

    public static DomainError UnsupportExtension(string extension)
    {
        return new(
            Code: "image_path.unsupport_extension",
            Message: $"Image path unsupport {extension} extension.");
    }

    public static DomainError InvalidImagePathFormat()
    {
        return new(
            Code: "image_path.invalid_image_path_format",
            Message: "Image path is invalid format.");
    }
}