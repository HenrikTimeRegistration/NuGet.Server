using NuGet.Service.Core.ResoultObject;

namespace NuGet.Service.Core;

public interface INugetPackageCRUD
{
    public Task AddNugetPackageAsync(Stream location, NuGetIdentity identity);
    public Task DeleteNugetPackage(NuGetIdentity identity);
    public Task<Stream> GetNugetPackageAsync(NuGetIdentity identity);
}
