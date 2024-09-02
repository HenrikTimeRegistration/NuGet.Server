using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Service.Core.Interfaces.Logic;
using NuGet.Service.Core.ResoultObject;
using System.Net;
using System.Security.Principal;

namespace NuGet.WebApp.Controllers;

public class NuGetPackageUpdate : ControllerBase
{
    public NuGetPackageUpdate(ILogger<NugetPackage> log, IPackageCreateAndUpdate packageCreateAndUpdate)
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
    public async Task<IActionResult> DeletePackageAsync(string id, string version, CancellationToken token = default)
    {
        var identity = new NuGetIdentity() { Id = id, Version = version };
        await PackageCreateAndUpdate.DeletePackageAsync(identity, token);
        return new StatusCodeResult(StatusCodes.Status204NoContent);
    }

    [Route("v2/package/{id}/{version}")]
    [HttpPost]
    public async Task<IActionResult> PostRelistPackageAsync(string id, string version, CancellationToken token = default)
    {
        var identity = new NuGetIdentity() { Id = id, Version = version };
        await PackageCreateAndUpdate.RelistPackageAsync(identity, token);
        return new OkResult();
    }
}