using Newtonsoft.Json.Linq;
using NuGet.Service.Core.Interfaces.Logic;
using NuGet.Service.Core.Interfaces.Storage;
using NuGet.Service.Core.ResoultObject;
using NuGet.Service.Result;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service;
public class PackageData : IPackageData
{
    public PackageData(IGetNuGetPackageInfo packageInfo, INugetPackageCRUD nuGetPackageCRUD) 
    {
        PackageInfo = packageInfo;
        NuGetPackageCRUD = nuGetPackageCRUD;
    }

   private IGetNuGetPackageInfo PackageInfo { get; set; }

    private INugetPackageCRUD NuGetPackageCRUD { get; set; }

    public async Task<NuGetVersions> GetListOfVersionsAsync(string id)
    {
        var packages = await PackageInfo.GetListOfPackagesAsync(id) ?? throw new NullReferenceException();
        return new NuGetVersions() { versions = packages.Versions.ToList() };
    }

    public async Task<string> GetNuspec(NuGetIdentity nuGetIdentity)
    {
        using var stream = await NuGetPackageCRUD.GetNugetPackageAsync(nuGetIdentity);
        using var files = new ZipArchive(stream);
        var entry = files.Entries.FirstOrDefault(x => x.Name.ToLower().EndsWith($"{nuGetIdentity.Id}{IPackageData.NuspaceExtension}")) ?? throw new NullReferenceException();
        using var streamNuspace = entry.Open();
        using var reader = new StreamReader(streamNuspace);
        return reader.ReadToEnd();
    }
}
