using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Service.Core.Interfaces.Logic;
using System.Net;

namespace NuGet.Functions;

public class NugetPackageUpdate : ControllerBase
{
    public NugetPackageUpdate(ILogger<NugetPackage> log, IPackageCreateAndUpdate packageCreateAndUpdate)
    {
        Logger = log;
        PackageCreateAndUpdate = packageCreateAndUpdate;
    }

    private ILogger<NugetPackage> Logger { get; init; }

    private IPackageCreateAndUpdate PackageCreateAndUpdate { get; init; }

    [Route("v2/package")]
    [HttpPut]
    public async Task<IActionResult> PutPackageAsync(IFormFile file, CancellationToken token = default)
    {
        await PackageCreateAndUpdate.UploadPackageAsync(file.OpenReadStream());
        return new OkResult();
    }

    [Route("v2/package/{id}/{version}")]
    [HttpDelete]
    public async Task<IActionResult> DeletePackageAsync( string id, string version, CancellationToken token = default)
    {
        id = id.ToLower();
        version = version.ToLower();
        await PackageCreateAndUpdate.DeletePackageAsync(id, version);
        return new StatusCodeResult(StatusCodes.Status204NoContent);
    }

    [Route("v2/package/{id}/{version}")]
    [HttpPost]
    public async Task<IActionResult> PostRelistPackageAsync(string id, string version, CancellationToken token = default)
    {
        id = id.ToLower();
        version = version.ToLower();
        await PackageCreateAndUpdate.RelistPackageAsync(id, version);
        return new OkResult();
    }
}