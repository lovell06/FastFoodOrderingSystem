using FastFoodOrderingSystem.Domain.Common.DomainResults;

namespace FastFoodOrderingSystem.Domain.Categories.ValueObjects.Errors;

public static class InvalidCategoryDescriptionError
{
    public static DomainError ExceedsMaxLength(int maxLength)
    {
        return new DomainError(
            "invalid_category_description_error.exceeds_max_length",
            $"Category description must not exceeds {maxLength} characters.");
    }
}