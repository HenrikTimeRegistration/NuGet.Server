using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.Functions;

public class NugetPackage
{
    public NugetPackage(ILogger<NugetPackage> log)
    {
        Logger = log;
    }

    private ILogger<NugetPackage> Logger { get; init; }

    [FunctionName(nameof(GetPackagesAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiParameter("id", Description = "The package name")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> GetPackagesAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v3-Packages/{id}/index.josn")]
        HttpRequest req,
        string id,
        CancellationToken token = default)
    {
        await Task.CompletedTask;

        return new OkResult();
    }

    [FunctionName(nameof(GetPackageAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiParameter("id", Description = "The package name")]
    [OpenApiParameter("version", Description = "The package version")]
    [OpenApiParameter("combined", Description = "Combined id.version")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> GetPackageAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v3-Packages/{id}/{version}/{combined}.nupkg")]
        HttpRequest req,
        string id,
        string version,
        string combined,
        CancellationToken token = default)
    {
        if (!combined.Equals($"{id}.{version}"))
        {
            return new NotFoundObjectResult("id and version don't match requested nupkg");
        }
        await Task.CompletedTask;

        return new OkResult();
    }

    [FunctionName(nameof(GetPackageManifestAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiParameter("id", Description = "The package name")]
    [OpenApiParameter("version", Description = "The package version")]
    [OpenApiParameter("name", Description = "The package name same as the Id")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> GetPackageManifestAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v3-Packages/{id}/{version}/{name}.nuspec")]
        HttpRequest req,
        string id,
        string version,
        string name,
        CancellationToken token = default)
    {
        if (!name.Equals($"{id}"))
        {
            return new NotFoundObjectResult("id don't match");
        }
        await Task.CompletedTask;

        return new OkResult();
    }
}

