using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using NuGet.Service.Core.Interfaces.Logic;
using NuGet.Service.Core.Interfaces.Storage;
using NuGet.Service.Core.ResoultObject;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.Functions;

public class NugetPackage
{
    public NugetPackage(ILogger<NugetPackage> log, IPackageData packageData, INugetPackageCRUD packageStoreage)
    {
        Logger = log;
    }

    private ILogger<NugetPackage> Logger { get; init; }

    private IPackageData PackageData { get; init; }

    private INugetPackageCRUD packageStoreage { get; init; }

    [Function(nameof(GetPackagesAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiParameter("id", Description = "The package name")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> GetPackagesAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v3-Packages/{id}/index.josn")]
        HttpRequest req,
        string id,
        CancellationToken token = default)
    {
        id = id.ToLower();
        return new OkObjectResult(await PackageData.GetListOfVersionsAsync(id));
    }

    [Function(nameof(GetPackageAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiParameter("id", Description = "The package name")]
    [OpenApiParameter("version", Description = "The package version")]
    [OpenApiParameter("combined", Description = "Combined id.version")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> GetPackageAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v3-Packages/{id}/{version}/{combined}.nupkg")]
        HttpRequest req,
        string id,
        string version,
        string combined,
        CancellationToken token = default)
    {
        var identity = new NuGetIdentity() { Id = id, Version = version };
        combined = combined.ToLower();
        if (!combined.Equals($"{identity.Id}.{identity.Version}"))
        {
            return new StatusCodeResult(StatusCodes.Status400BadRequest);
        }
        await Task.CompletedTask;


        return new FileStreamResult(await packageStoreage.GetNugetPackageAsync(identity), "multipart/form-data");
    }

    [Function(nameof(GetPackageManifestAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiParameter("id", Description = "The package name")]
    [OpenApiParameter("version", Description = "The package version")]
    [OpenApiParameter("name", Description = "The package name same as the Id")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> GetPackageManifestAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v3-Packages/{id}/{version}/{name}.nuspec")]
        HttpRequest req,
        string id,
        string version,
        string name,
        CancellationToken token = default)
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

