using NuGet.Service.Core.Interfaces.Logic;
using NuGet.Service.Core.Interfaces.Storage;
using NuGet.Service.Core.ResoultObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service;
public class PackageData : IPackageData
{
    public PackageData(IGetNuGetPackageInfo packageInfo) 
    {
        PackageInfo = packageInfo;
    }

   private IGetNuGetPackageInfo PackageInfo { get; set; }

    public async Task<List<string>> GetListOfVersionsAsync(string id)
    {
        var packages = await PackageInfo.GetListOfPackagesAsync(id) ?? throw new NullReferenceException();
        return packages.Versions.ToList();
    }
}
