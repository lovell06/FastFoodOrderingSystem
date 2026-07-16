using FastFoodOrderingSystem.Application.Abstractions.storage;
using Microsoft.AspNetCore.Hosting;

namespace FastFoodOrderingSystem.Infrastructure.Storage;

public sealed class LocalFileStorage : IFileStorage
{
    private readonly string _root;

    public LocalFileStorage(IWebHostEnvironment env)
    {
        _root = env.WebRootPath;
    }

    public Task DeleteAsync(string key, CancellationToken cancellationToken)
    {
        var fullPath = Path.Combine(_root, key);

        if (File.Exists(fullPath))
            File.Delete(fullPath);
        
        return Task.CompletedTask;
    }

    public async Task<string> UploadAsync(
        Stream stream, 
        FileStorageOptions options, 
        CancellationToken cancellationToken)
    {
        var fileName = $"{Guid.NewGuid():N}{options.Extension}";

        var folderName = options.Category switch
        {
            FileStorageCategory.Avatar => "avatars",
            FileStorageCategory.Product => "products",
            _ => throw new ArgumentOutOfRangeException()
        };

        var key = Path.Combine(folderName, fileName);

        var fullPath = Path.Combine(_root, key);

        await using var fileStream = new FileStream(
            path: fullPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.Read,
            bufferSize: 8192,
            useAsync: true);

        await stream.CopyToAsync(fileStream, cancellationToken);

        return key;
    }
}