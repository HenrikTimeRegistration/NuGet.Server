using Microsoft.AspNetCore.Mvc;
using NuGet.Service.Core.Interfaces.Logic;
using NuGet.Service.Core.Interfaces.Storage;
using NuGet.Service.Core.ResoultObject;
using System.Net;

namespace NuGet.Functions;

public class NugetPackage : ControllerBase
{
    public NugetPackage(ILogger<NugetPackage> log, IPackageData packageData, INugetPackageCRUD packageStoreage)
    {
        Logger = log;
        PackageData = packageData;
    }

    private ILogger<NugetPackage> Logger { get; init; }

    private IPackageData PackageData { get; init; }

    private INugetPackageCRUD packageStoreage { get; init; }

    [Route("v3-Packages/{id}/index.josn")]
    [HttpGet]
    public async Task<IActionResult> GetPackagesAsync(string id, CancellationToken token = default)
    {
        id = id.ToLower();
        return new OkObjectResult(await PackageData.GetListOfVersionsAsync(id));
    }

    [Route("v3-Packages/{id}/{version}/{combined}.nupkg")]
    [HttpGet]
    public async Task<IActionResult> GetPackageAsync(string id, string version, string combined, CancellationToken token = default)
    {
        var identity = new NuGetIdentity() { Id = id, Version = version };
        combined = combined.ToLower();
        if (!combined.Equals($"{identity.Id}.{identity.Version}"))
        {
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        return new FileStreamResult(await packageStoreage.GetNugetPackageAsync(identity), "multipart/form-data");
    }

    [Route("v3-Packages/{id}/{version}/{name}.nuspec")]
    [HttpGet]
    public async Task<IActionResult> GetPackageManifestAsync(
        string id, string version, string name, CancellationToken token = default)
    {
        id = id.ToLower();
        version = version.ToLower();
        name = name.ToLower();
        if (!name.Equals($"{id}"))
        {
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
        await Task.CompletedTask;

        return new OkResult();
    }
}

