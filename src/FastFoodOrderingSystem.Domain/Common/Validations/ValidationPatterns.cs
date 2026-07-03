using System.Text.RegularExpressions;

namespace FastFoodOrderingSystem.Domain.Common.Validations;

public static class ValidationPatterns
{
    public const string Email 
        = @"^[A-Za-z0-9](?:[A-Za-z0-9._%+-]*[A-Za-z0-9])?@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)+$";

    public const string FullName = @"[^\p{L}\s]";

    public static string ImagePath(string extension) =>
        $"^images/(?:[A-Za-z0-9_-]+/)*[A-Za-z0-9_-]+{Regex.Escape(extension)}$";

    public const string PhoneNumber = @"\D";
}