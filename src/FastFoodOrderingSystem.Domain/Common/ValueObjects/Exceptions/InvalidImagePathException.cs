using FastFoodOrderingSystem.Domain.Common.Exceptions;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

public class InvalidImagePathException : DomainException
{
    private InvalidImagePathException(string code, string message) : base(code, message)
    {
    }

    public static InvalidImagePathException Empty()
    {
        return new InvalidImagePathException(
            code: "image_path.empty",
            message: "Image path must not be empty.");
    }

    public static InvalidImagePathException ExceedsMaxLength(int maxLength)
    {
        return new InvalidImagePathException(
            code: "image_path.exceeds_max_length",
            message: $"Image path must not exceed {maxLength} characters.");
    }

    public static InvalidImagePathException UnsupportExtension(string extension)
    {
        return new InvalidImagePathException(
            code: "image_path.unsupport_extension",
            message: $"Image path unsupport {extension} extension.");
    }

    public static InvalidImagePathException InvalidImagePathFormat()
    {
        return new InvalidImagePathException(
            code: "image_path.invalid_image_path_format",
            message: "Image path is invalid format.");
    }
}