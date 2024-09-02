using Microsoft.Extensions.Logging;
using Nuget.FileStorage.Data;
using NuGet.Service.Core;
using NuGet.Service.Core.Interfaces.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuget.FileStorage;
public class GetNuGetPackageInfo : IGetNuGetPackageInfo
{
    public GetNuGetPackageInfo(ILogger<GetNuGetPackageInfo> logger, DirectoryOptions directoryOptions)
    {
        Logger = logger;
        DirectoryOptions = directoryOptions;
    }

    private ILogger<GetNuGetPackageInfo> Logger { get; init; }

    private DirectoryOptions DirectoryOptions { get; init; }

    public async Task<NuGetPackageData> GetListOfPackagesAsync(string id)
    {
        await Task.CompletedTask;
        var path = Path.Combine(DirectoryOptions.BaseFilePath, DirectoryOptions.Prefix + id);
        string[] directoryes = Directory.GetDirectories(path) ?? throw new NullReferenceException();
        var packages = new NuGetPackageData() { Name = id, Versions = directoryes.Select(x=> x.Replace(Path.Combine(path, DirectoryOptions.Prefix), string.Empty)) };
        return packages;
    }
}
