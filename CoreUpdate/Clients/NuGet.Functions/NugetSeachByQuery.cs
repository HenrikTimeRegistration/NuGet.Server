using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NuGet.Functions.Extensions;
using System.Net;
using System.Threading.Tasks;


namespace NuGet.Functions;

public class NugetSeachByQuery
{
    private readonly ILogger<NugetSeachByQuery> _logger;

    public NugetSeachByQuery(ILogger<NugetSeachByQuery> log)
    {
        _logger = log;
    }

    [Function(nameof(GetPackagesBuQuerySeach))]
    [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
    [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public async Task<IActionResult> GetPackagesBuQuerySeach(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "query")] HttpRequest req)
    {
        var secretsTokens = req.Query.GetSecretsTokens();
        return new OkResult();
    }
}

