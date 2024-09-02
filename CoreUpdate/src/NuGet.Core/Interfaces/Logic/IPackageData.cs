using NuGet.Service.Core.ResoultObject;
using NuGet.Service.Result;
using NuGet.Versioning;

namespace NuGet.Service.Core.Interfaces.Logic;

public interface IPackageData
{
    public const string NuspaceExtension = ".nuspec";

    public Task<NuGetVersions> GetListOfVersionsAsync(string id);

    public Task<string> GetNuspec(NuGetIdentity nuGetIdentity);
}
