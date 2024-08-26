using NuGet.Service.Core;
using System.IO.Compression;

namespace NuGet.Service;
public class PackageCreateAndUpdate : IPackageCreateAndUpdate
{
    PackageCreateAndUpdate (INugetPackageCRUD storeage)
    {
        Storeage = storeage;
    }

    private const string nusoec = ".nuspec";

    INugetPackageCRUD Storeage { get; set; }

    public Task DeletePackageAsync(string packageName, string packageVersion)
    {
        throw new NotImplementedException();
    }

    public Task RelistPackageAsync(string packageName, string packageVersion)
    {
        throw new NotImplementedException();
    }

    public async Task UploadPackageAsync(Stream stream)
    {
        using var arciv = new ZipArchive(stream);
        foreach (ZipArchiveEntry entry in arciv.Entries)
        {
            if(entry.FullName.EndsWith(nusoec, StringComparison.OrdinalIgnoreCase))
            {
                using StreamReader reader = new StreamReader(entry.Open());
                string text = await reader.ReadToEndAsync();
            }
        }
    }
}
