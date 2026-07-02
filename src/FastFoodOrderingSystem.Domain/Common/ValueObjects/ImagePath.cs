using System.Text.RegularExpressions;
using FastFoodOrderingSystem.Domain.Common.ValueObjects.Exceptions;

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

    private static void Validate(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw InvalidImagePathException.Empty();

        path = path.Trim();
        if (path.Length > MaxLength)
            throw InvalidImagePathException.ExceedsMaxLength(MaxLength);

        string extension = Path.GetExtension(path);
        if (!SupportedExtensions.Contains(extension))
            throw InvalidImagePathException.UnsupportExtension(extension);

        if (!Regex.IsMatch(path, $"^images/(?:[A-Za-z0-9_-]+/)*[A-Za-z0-9_-]+{Regex.Escape(extension)}$"))
            throw InvalidImagePathException.InvalidImagePathFormat();
    }

    public static ImagePath Create(string path)
    {
        Validate(path);
        return new ImagePath(path);
    }

    public static ImagePath Default()
    {
        return new ImagePath("default");
    }
}