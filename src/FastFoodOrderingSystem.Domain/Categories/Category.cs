using FastFoodOrderingSystem.Domain.Categories.ValueObjects;
using FastFoodOrderingSystem.Domain.Common.Abstractions;

namespace FastFoodOrderingSystem.Domain.Categories;

public class Category : AggregateRoot<CategoryId>
{
    public CategoryName Name { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public bool IsDeleted => DeletedAt != null;
    public CategoryDescription Description { get; private set; }

    protected Category()
    {
    }

    private Category(
        CategoryId id,
        CategoryName name,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        CategoryDescription description)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
        Description = description;
    }

    public static Category Create(
        CategoryName name,
        DateTime createdAt,
        DateTime? updatedAt,
        DateTime? deletedAt,
        CategoryDescription description)
    {
        return new Category(
            id: CategoryId.Default,
            name: name,
            createdAt: createdAt,
            updatedAt: updatedAt,
            deletedAt: deletedAt,
            description: description);
    }

    public void ChangeName(CategoryName newName, DateTime updatedAt)
    {
        Name = newName;
        UpdatedAt = updatedAt;
    }

    public void ChangeDescription(CategoryDescription newDescription, DateTime updatedAt)
    {
        Description = newDescription;
        UpdatedAt = updatedAt;
    }

    public void Delete(DateTime updatedAt)
    {
        DeletedAt = updatedAt;
        UpdatedAt = updatedAt;
    }

    public void Restore(DateTime updatedAt)
    {
        DeletedAt = null;
        UpdatedAt = updatedAt;
    }
}