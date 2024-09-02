using NuGet.Service.Core.ResoultObject;

namespace NuGet.Service.Core.Interfaces.Logic;

public interface IPackageData
{
    public Task<List<string>> GetListOfVersionsAsync(string id);
}
