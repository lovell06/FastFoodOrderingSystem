namespace FastFoodOrderingSystem.Application.Abstractions.storage;

public interface IFileStorage
{
    Task<string> UploadAsync(
        Stream stream,
        FileStorageOptions options,
        CancellationToken cancellationToken);

    Task DeleteAsync(
        string key,
        CancellationToken cancellationToken);
}