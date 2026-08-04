using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Categories.ValueObjects.Errors;

public static class InvalidCategoryNameError
{
    public static DomainError Empty()
    {
        return new DomainError(
            "invalid_category_name_error.empty",
            "Category name must be not empty.");
    }

    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new DomainError(
            "invalid_category_name_error.exceeds_max_length",
            $"Category must not exceeds {maxLength} characters.");
    }
}