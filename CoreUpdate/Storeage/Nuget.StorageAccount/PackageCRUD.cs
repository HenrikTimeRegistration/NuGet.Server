using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using NuGet.Service.Core.Exceptions;
using NuGet.Service.Core.Interfaces.Storage;
using NuGet.Service.Core.ResoultObject;

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

    public async Task AddNugetPackageAsync(Stream location, NuGetIdentity identity)
    {
        var containerClient = BlobServiceClient.GetBlobContainerClient(identity.Id);
        containerClient.CreateIfNotExists();
        var blob = containerClient.GetBlobClient(identity.Version);
        if (!blob.Exists())
        {
            throw new NugetPackageAlreadyExistException();
        }
        await blob.UploadAsync(location);
    }

    public async Task DeleteNugetPackage(NuGetIdentity identity)
    {
        var containerClient = BlobServiceClient.GetBlobContainerClient(identity.Id);
        if (!containerClient.Exists())
        {
            return;
        }
        await containerClient.DeleteBlobIfExistsAsync(identity.Version);
    }

    public async Task<Stream> GetNugetPackageAsync(NuGetIdentity identity)
    {
        var containerClient = BlobServiceClient.GetBlobContainerClient(identity.Id);
        if (containerClient.Exists())
        {
            throw new FileNotFoundException();
        }

        var BlobClient = containerClient.GetBlobClient(identity.Version);
        return await BlobClient.OpenReadAsync();
    }
}
