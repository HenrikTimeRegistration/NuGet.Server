using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nuget.FileStorage.Data;
using NuGet.Service.Core.Exceptions;
using NuGet.Service.Core.Interfaces.Storage;
using NuGet.Service.Core.ResoultObject;
using System.IO;

namespace Nuget.FileStorage;

public class PackageCRUD : INugetPackageCRUD
{
    private const string Nupkg = ".nupkg";
    private const string package = "nugetPackage";

    public PackageCRUD(ILogger<PackageCRUD> logger, DirectoryOptions directoryOptions)
    {
        Logger = logger;
        DirectoryOptions = directoryOptions;
    }

    private ILogger<PackageCRUD> Logger { get; init; }

    private DirectoryOptions DirectoryOptions { get; init; }

    public async Task AddNugetPackageAsync(Stream location, NuGetIdentity identity)
    {
        var path = Path.Combine(DirectoryOptions.BaseFilePath, DirectoryOptions.Prefix + identity.Id, DirectoryOptions.Prefix + identity.Version);
        Directory.CreateDirectory(path);
        path = Path.Combine(path, DirectoryOptions.Package + Nupkg);
        if (File.Exists(path))
        {
            throw new NugetPackageAlreadyExistException();
        }

        using var fileStream = File.Create(path);
        location.Seek(0, SeekOrigin.Begin);
        await location.CopyToAsync(fileStream);
    }

    public async Task DeleteNugetPackage(NuGetIdentity identity)
    {
        await Task.CompletedTask;
        var path = Path.Combine(DirectoryOptions.BaseFilePath, DirectoryOptions.Prefix + identity.Id, DirectoryOptions.Prefix + identity.Version, DirectoryOptions.Package + Nupkg);
        File.Delete(path);
    }

    public async Task<Stream> GetNugetPackageAsync(NuGetIdentity identity)
    {
        await Task.CompletedTask;
        var path = Path.Combine(DirectoryOptions.BaseFilePath, DirectoryOptions.Prefix + identity.Id, DirectoryOptions.Prefix + identity.Version, DirectoryOptions.Package + Nupkg);
        return File.OpenRead(path);
    }
}
