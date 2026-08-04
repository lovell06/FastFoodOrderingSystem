using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.Validations;
using FastFoodOrderingSystem.Domain.Users.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Users.ValueObjects;

public record struct AvatarImagePath
{
    public const int MaxLength = 255;

    private static readonly string[] SupportedExtensions =
    [
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    ];

    public string Value { get; init; }

    private AvatarImagePath(string value)
    {
        Value = value.Trim();
    }

    private static DomainError? Validate(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return AvatarImagePathError.Empty();

        path = path.Trim();
        if (path.Length > MaxLength)
            return AvatarImagePathError.ExceedsMaxLength(MaxLength);

        string extension = Path.GetExtension(path);
        if (!SupportedExtensions.Contains(extension))
            return AvatarImagePathError.UnsupportExtension(extension);

        if (!Regex.IsMatch(path, ValidationPatterns.ImagePath(extension)))
            return AvatarImagePathError.InvalidImagePathFormat();

        return null;
    }

    public static DomainResult<AvatarImagePath> Create(string path)
    {
        var error = Validate(path);

        if (error is not null)
            return DomainResult<AvatarImagePath>.Failure(error);
        
        return DomainResult<AvatarImagePath>.Success(new AvatarImagePath(path));
    }

    public static AvatarImagePath Default()
    {
        return new AvatarImagePath("images/users/default.png");
    }
}