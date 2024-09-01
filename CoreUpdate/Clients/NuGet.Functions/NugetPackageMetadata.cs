using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;

namespace NuGet.Functions;

public class NugetPackageMetadata
{
    private readonly ILogger<NugetPackageMetadata> _logger;

    public NugetPackageMetadata(ILogger<NugetPackageMetadata> log)
    {
        _logger = log;
    }

    [Function("GetPackageMetadata")]
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
        id = id.ToLower();
        await Task.CompletedTask;
        return new OkResult();
    }
}

