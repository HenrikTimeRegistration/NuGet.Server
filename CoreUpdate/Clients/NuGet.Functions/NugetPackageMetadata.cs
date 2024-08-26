using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace NuGet.Functions;

public class NugetPackageMetadata
{
    private readonly ILogger<NugetPackageMetadata> _logger;

    public NugetPackageMetadata(ILogger<NugetPackageMetadata> log)
    {
        _logger = log;
    }

    [FunctionName("GetPackageMetadata")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> GetPackageMetadata(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "v3/PackageMetadata/{id}/index.josn")]
        HttpRequest req,
        string id,
        CancellationToken token = default)
    {
        await Task.CompletedTask;
        return new OkResult();
    }
}

