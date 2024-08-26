using NuGet.Service.Core.ResoultObject;

namespace NuGet.Service.Core;
public interface PackageData
{
    public Task<PackageVersions> GetListOfVersionsAsync(string id);
}
