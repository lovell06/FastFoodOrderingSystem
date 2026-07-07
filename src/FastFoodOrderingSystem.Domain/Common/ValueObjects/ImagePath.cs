using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.Validations;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Errors;

namespace FastFoodOrderingSystem.Domain.Common.ValueObjects;

public record ImagePath
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

    private ImagePath(string value)
    {
        Value = value.Trim();
    }

    private static DomainError? Validate(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return ImagePathError.Empty();

        path = path.Trim();
        if (path.Length > MaxLength)
            return ImagePathError.ExceedsMaxLength(MaxLength);

        string extension = Path.GetExtension(path);
        if (!SupportedExtensions.Contains(extension))
            return ImagePathError.UnsupportExtension(extension);

        if (!Regex.IsMatch(path, ValidationPatterns.ImagePath(extension)))
            return ImagePathError.InvalidImagePathFormat();

        return null;
    }

    public static DomainResult<ImagePath> Create(string path)
    {
        var error = Validate(path);

        if (error is not null)
            return DomainResult<ImagePath>.Failure(error);
        
        return DomainResult<ImagePath>.Success(new ImagePath(path));
    }

    public static ImagePath Default()
    {
        return new ImagePath("images/users/default.png");
    }
}