using FastFoodOrderingSystem.Domain.Categories.ValueObjects;
using FastFoodOrderingSystem.Domain.Common.Abstractions;
using FastFoodOrderingSystem.Domain.Common.DomainResults;
using FastFoodOrderingSystem.Domain.Common.ValueObjects;
using FastFoodOrderingSystem.Domain.Products.Enums;
using FastFoodOrderingSystem.Domain.Products.Errors;
using FastFoodOrderingSystem.Domain.Products.ValueObjects;

namespace FastFoodOrderingSystem.Domain.Products;

public class Product : AggregateRoot<ProductId>
{
    public CategoryId CategoryId { get; private set; }
    public ProductName Name { get; private set; }
    public Money Price { get; private set; }
    public StockQuantity StockQuantity { get; private set; }
    public bool IsOutOfStock => StockQuantity == StockQuantity.Zero;
    public ProductDescription Description { get; private set; }
    public ProductImagePath ImagePath { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted => Status == ProductStatus.Deleted;
    public bool IsDiscontinued => Status == ProductStatus.Discontinued;
    public ProductStatus Status { get; private set; }
    public bool IsVisible { get; private set; }

    protected Product()
    {
    }

    private Product(
        ProductId id,
        CategoryId categoryId,
        ProductName name,
        Money price,
        StockQuantity stockQuantity,
        ProductDescription description,
        ProductImagePath imagePath,
        DateTime createdAt,
        DateTime? updatedAt,
        ProductStatus status,
        bool isVisible)
    {
        Id = id;
        CategoryId = categoryId;
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        Description = description;
        ImagePath = imagePath;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        Status = status;
        IsVisible = isVisible;
    }

    public static Product Create(
        CategoryId categoryId,
        ProductName name,
        Money price,
        StockQuantity stockQuantity,
        ProductDescription description,
        ProductImagePath imagePath,
        DateTime createdAt)
    {
        return new Product(
            id: ProductId.Default,
            categoryId: categoryId,
            name: name,
            price: price,
            stockQuantity: stockQuantity,
            description: description,
            imagePath: imagePath,
            createdAt: createdAt,
            updatedAt: null,
            status: ProductStatus.Active,
            isVisible: true);
    }

    public void ChangeCategory(CategoryId categoryId, DateTime updatedAt)
    {
        CategoryId = categoryId;
        UpdatedAt = updatedAt;
    }

    public void ChangeName(ProductName name, DateTime updatedAt)
    {
        Name = name;
        UpdatedAt = updatedAt;
    }

    public void ChangePrice(Money price, DateTime updatedAt)
    {
        Price = price;
        UpdatedAt = updatedAt;
    }

    public DomainResult<Unit> IncreaseStockQuantity(int amount, DateTime updatedAt)
    {
        if (Status != ProductStatus.Active)
            return DomainResult<Unit>.Failure(InvalidProductError.Inactive());
        
        var res = StockQuantity.Increase(amount);
        if (res.IsFailure)
            return DomainResult<Unit>.Failure(res.Error);

        StockQuantity = res.Value;
        UpdatedAt = updatedAt;
        
        return DomainResult<Unit>.Success(Unit.Value);
    }

    public DomainResult<Unit> DecreaseStockQuantity(int amount, DateTime updatedAt)
    {
        if (Status != ProductStatus.Active)
            return DomainResult<Unit>.Failure(InvalidProductError.Inactive());

        var res = StockQuantity.Decrease(amount);
        if (res.IsFailure)
            return DomainResult<Unit>.Failure(res.Error);

        StockQuantity = res.Value;
        UpdatedAt = updatedAt;

        return DomainResult<Unit>.Success(Unit.Value);
    }

    public DomainResult<Unit> Hide(DateTime updatedAt)
    {
        if (IsDeleted)
            return DomainResult<Unit>.Failure(InvalidProductError.Deleted());
        
        if (!IsVisible)
            return DomainResult<Unit>.Success();

        IsVisible = false;
        UpdatedAt = updatedAt;
        
        return DomainResult<Unit>.Success();
    }

    public DomainResult<Unit> Unhide(DateTime updatedAt)
    {
        if (IsDeleted)
            return DomainResult<Unit>.Failure(InvalidProductError.Deleted());
        
        if (IsVisible)
            return DomainResult<Unit>.Success();

        IsVisible = true;
        UpdatedAt = updatedAt;
        
        return DomainResult<Unit>.Success();
    }

    public DomainResult<Unit> Delete(DateTime deletedAt)
    {
        if (IsDeleted)
            return DomainResult<Unit>.Success();
        
        Status = ProductStatus.Deleted;
        IsVisible = false;
        UpdatedAt = deletedAt;
        
        return DomainResult<Unit>.Success();
    }
    
    public DomainResult<Unit> Restore(DateTime restoredAt)
    {
        if (!IsDeleted)
            return DomainResult<Unit>.Success();

        Status = ProductStatus.Active;
        IsVisible = true;
        UpdatedAt = restoredAt;
        
        return DomainResult<Unit>.Success();
    }

    public DomainResult<Unit> Discontinue(DateTime discontinuedAt)
    {
        if (IsDeleted)
            return DomainResult<Unit>.Failure(InvalidProductError.Deleted());
        
        if (IsDiscontinued)
            return DomainResult<Unit>.Success();
        
        Status = ProductStatus.Discontinued;
        UpdatedAt = discontinuedAt;
        
        return DomainResult<Unit>.Success();
    }

    public DomainResult<Unit> Continue(DateTime continuedAt)
    {
        if (IsDeleted)
            return DomainResult<Unit>.Failure(InvalidProductError.Deleted());
        
        if (!IsDiscontinued) 
            return DomainResult<Unit>.Success();
        
        Status = ProductStatus.Active;
        UpdatedAt = continuedAt;

        return DomainResult<Unit>.Success();
    }
}