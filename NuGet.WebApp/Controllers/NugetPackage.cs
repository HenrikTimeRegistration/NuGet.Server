using Microsoft.AspNetCore.Mvc;
using NuGet.Service.Core.Interfaces.Logic;
using NuGet.Service.Core.Interfaces.Storage;
using NuGet.Service.Core.ResoultObject;
using System.Net;

namespace NuGet.WebApp.Controllers;

public class NugetPackage : ControllerBase
{
    public NugetPackage(ILogger<NugetPackage> log, IPackageData packageData, INugetPackageCRUD packageStorage)
    {
        Logger = log;
        PackageData = packageData;
        PackageStorage = packageStorage;
    }

    private ILogger<NugetPackage> Logger { get; init; }

    private IPackageData PackageData { get; init; }

    private INugetPackageCRUD PackageStorage { get; init; }

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

        return new FileStreamResult(await PackageStorage.GetNugetPackageAsync(identity), "multipart/form-data");
    }

    [Route("v3-Packages/{id}/{version}/{name}.nuspec")]
    [HttpGet]
    public async Task<IActionResult> GetPackageManifestAsync(
        string id, string version, string name, CancellationToken token = default)
    {
        var identity = new NuGetIdentity() { Id = id, Version = version };
        if (!name.ToLower().Equals($"{identity.Id}"))
        {
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }

        return new OkObjectResult(await PackageData.GetNuspec(identity));
    }
}

