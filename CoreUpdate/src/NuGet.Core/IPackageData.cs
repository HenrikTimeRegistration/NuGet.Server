using NuGet.Service.Core.ResoultObject;

namespace NuGet.Service.Core;

public interface IPackageData
{
    public Task<PackageVersions> GetListOfVersionsAsync(string id);
}
