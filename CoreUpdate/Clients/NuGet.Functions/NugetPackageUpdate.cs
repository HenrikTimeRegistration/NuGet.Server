using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.Functions;
public class NugetPackageUpdate
{
    public NugetPackageUpdate(ILogger<NugetPackage> log)
    {
        Logger = log;
    }

    private ILogger<NugetPackage> Logger { get; init; }

    [FunctionName(nameof(PutPackageAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> PutPackageAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v2/package")]
        HttpRequest req,
        CancellationToken token = default)
    {
        await Task.CompletedTask;

        return new OkResult();
    }

    [FunctionName(nameof(DeletePackageAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiParameter("id", Description = "The package name")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> DeletePackageAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "v2/package/{id}/{version}")]
        HttpRequest req,
        string id,
        string version,
        CancellationToken token = default)
    {
        await Task.CompletedTask;

        return new OkResult();
    }

    [FunctionName(nameof(PostPackageAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiParameter("id", Description = "The package name")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> PostPackageAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v2/package/{id}/{version}")]
        HttpRequest req,
        string id,
        string version,
        CancellationToken token = default)
    {
        await Task.CompletedTask;

        return new OkResult();
    }
}
