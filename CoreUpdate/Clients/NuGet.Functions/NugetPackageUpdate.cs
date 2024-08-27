using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using NuGet.Service.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Collections.Generic;

namespace NuGet.Functions;

public class NugetPackageUpdate
{
    public NugetPackageUpdate(ILogger<NugetPackage> log, IPackageCreateAndUpdate packageCreateAndUpdate)
    {
        Logger = log;
        PackageCreateAndUpdate = packageCreateAndUpdate;
    }

    private ILogger<NugetPackage> Logger { get; init; }

    private IPackageCreateAndUpdate PackageCreateAndUpdate { get; init; }

    [FunctionName(nameof(PutPackageAsync))]
    [OpenApiOperation(operationId: "NugetPackageUpdate", Visibility = OpenApiVisibilityType.Advanced)]
    [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(MultiPartFormDataModel), Required = true, Description = "Files to upload to Azure Storage")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
    public async Task<IActionResult> PutPackageAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v2/package")]
        HttpRequest req,
        CancellationToken token = default)
    {
        if (!req.ContentType.Equals("multipart/form-data"))
        {
            return new BadRequestResult();
        }

        await PackageCreateAndUpdate.UploadPackageAsync(req.Body);
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
        id = id.ToLower();
        version = version.ToLower();
        await PackageCreateAndUpdate.DeletePackageAsync(id, version);
        return new StatusCodeResult(StatusCodes.Status204NoContent);
    }

    [FunctionName(nameof(PostRelistPackageAsync))]
    [OpenApiOperation(operationId: "Packages")]
    [OpenApiParameter("id", Description = "The package name")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> PostRelistPackageAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v2/package/{id}/{version}")]
        HttpRequest req,
        string id,
        string version,
        CancellationToken token = default)
    {
        id = id.ToLower();
        version = version.ToLower();
        await PackageCreateAndUpdate.RelistPackageAsync(id, version);
        return new OkResult();
    }
}

public class MultiPartFormDataModel
{
    public byte[] FileUpload { get; set; }
}