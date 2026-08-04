using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

public static class InvalidAvatarImagePathError
{
    public static DomainError Empty()
    {
        return new(
            Code: "invalid_avatar_image_path_error.empty",
            Message: "Avatar image path must not be empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new(
            Code: "invalid_avatar_image_path_error.exceeds_max_length",
            Message: $"Avatar image path must not exceed {maxLength} characters.");
    }

    public static DomainError UnsupportedExtension(string extension)
    {
        return new(
            Code: "invalid_avatar_image_path_error.unsupported_extension",
            Message: $"Avatar image path unsupported {extension} extension.");
    }

    public static DomainError Format()
    {
        return new(
            Code: "invalid_avatar_image_path_error.format",
            Message: "Avatar image path is invalid format.");
    }
}