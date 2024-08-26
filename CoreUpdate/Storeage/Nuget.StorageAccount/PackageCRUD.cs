using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using NuGet.Service.Core;

namespace Nuget.StorageAccount;

public class PackageCRUD : INugetPackageCRUD
{
    public PackageCRUD(ILogger<PackageCRUD> logger, BlobServiceClient blobServiceClient)
    {
        Logger = logger;
        BlobServiceClient = blobServiceClient;
    }

    BlobServiceClient BlobServiceClient { get; init; }

    private ILogger<PackageCRUD> Logger { get; init; }

    public async Task AddNugetPackageAsync(Stream location, string id, string version)
    {
        var containerClient = BlobServiceClient.GetBlobContainerClient(id);
        containerClient.CreateIfNotExists();
        await containerClient.UploadBlobAsync(version, location);
    }

    public async Task DeleteNugetPackage(string id, string version)
    {
        var containerClient = BlobServiceClient.GetBlobContainerClient(id);
        if (!containerClient.Exists())
        {
            return;
        }
        await containerClient.DeleteBlobIfExistsAsync(version);
    }

    public async Task<Stream> GetNugetPackageAsync(string id, string version)
    {
        var containerClient = BlobServiceClient.GetBlobContainerClient(id);
        if (containerClient.Exists())
        {
            throw new FileNotFoundException();
        }

        var BlobClient = containerClient.GetBlobClient(version);
        return await BlobClient.OpenReadAsync();
    }
}
