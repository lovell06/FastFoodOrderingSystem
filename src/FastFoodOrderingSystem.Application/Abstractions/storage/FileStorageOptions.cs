namespace FastFoodOrderingSystem.Application.Abstractions.storage;

public sealed record FileStorageOptions(
    FileStorageCategory Category,
    string Extension,
    string ContentType);