namespace NuGet.Service.Core;

public interface INugetPackageCRUD
{
    public Task<Stream> GetNugetPackageAsync(string id, string version);

    public Task AddNugetPackageAsync(Stream location, string id, string version);

    public Task DeleteNugetPackage(string id, string version);
}
