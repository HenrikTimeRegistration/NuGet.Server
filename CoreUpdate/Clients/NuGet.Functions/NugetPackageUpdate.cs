using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using NuGet.Functions.Options;
using NuGet.Service.Core.Interfaces.Logic;
using NuGet.Service.Core.ResoultObject;
using NuGet.Service.Core.Result;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.Functions;

public class NuGetPackageUpdate
{
    public NuGetPackageUpdate(ILogger<NugetPackage> log, IPackageCreateAndUpdate packageCreateAndUpdate)
    {
        Logger = log;
        PackageCreateAndUpdate = packageCreateAndUpdate;
    }

    private ILogger<NugetPackage> Logger { get; init; }

    private IPackageCreateAndUpdate PackageCreateAndUpdate { get; init; }

    [Function(nameof(PutPackageAsync))]
    [OpenApiOperation(operationId: "NugetPackageUpdate", Visibility = OpenApiVisibilityType.Advanced)]
    [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(MultiPartFormDataModel), Required = true, Description = "Files to upload to Azure Storage")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
    public async Task<IActionResult> PutPackageAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v2/package")]
        HttpRequest req,
        CancellationToken token = default)
    {
        var files = req.Form.Files;
        var file = files[0];
        await PackageCreateAndUpdate.UploadPackageAsync(file.OpenReadStream());
        return new OkResult();
    }

    [Function(nameof(DeletePackageAsync))]
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
        var identity = new NuGetIdentity() { Id = id, Version = version };
        await PackageCreateAndUpdate.DeletePackageAsync(identity, token);
        return new StatusCodeResult(StatusCodes.Status204NoContent);
    }

    [Function(nameof(PostRelistPackageAsync))]
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
        var identity = new NuGetIdentity() { Id = id, Version = version };
        await PackageCreateAndUpdate.RelistPackageAsync(identity, token);
        return new OkResult();
    }
}

public class MultiPartFormDataModel
{
    public byte[] FileUpload { get; set; }
}