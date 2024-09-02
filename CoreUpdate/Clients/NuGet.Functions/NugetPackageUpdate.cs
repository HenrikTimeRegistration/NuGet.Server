using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using System.IO;
using NuGet.Service.Core.Interfaces.Logic;

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

    private const string pcakageTjek = "C:\\Users\\HenrikHallenberg\\Downloads\\newtonsoft.json.13.0.3.nupkg";

    [Function(nameof(PutPackageAsync)), RequestFormLimits(ValueLengthLimit = 104857600, MultipartBodyLengthLimit = 104857600)]
    [OpenApiOperation(operationId: "NugetPackageUpdate", Visibility = OpenApiVisibilityType.Advanced)]
    [OpenApiRequestBody(contentType: "multipart/form-data", bodyType: typeof(IFormFile), Required = true, Description = "Files to upload to Azure Storage")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK, Description = "The OK response")]
    public async Task<IActionResult> PutPackageAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v2/package")]
        HttpRequest req,
        CancellationToken token = default)
    {
        var files = req.Form.Files;
        var file = files[0];
        using MemoryStream ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var test = ms.ToArray();
        var refrens = File.ReadAllBytes(pcakageTjek);
        var resoult = test.Equals(refrens);
        Logger.LogInformation($"resoult {resoult}");
        await PackageCreateAndUpdate.UploadPackageAsync(req.Body);
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
        id = id.ToLower();
        version = version.ToLower();
        await PackageCreateAndUpdate.DeletePackageAsync(id, version);
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