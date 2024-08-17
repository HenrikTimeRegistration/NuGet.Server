using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NuGet.Service.Core;
using NuGet.Versioning;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace NuGet.Functions
{
    public class Nuget
    {
        private readonly ILogger<Nuget> Logger;
        public required IServerPackageRepository ServerRepository;
        
        public Nuget(ILogger<Nuget> log, IServerPackageRepository serverRepository)
        {
            Logger = log;
            ServerRepository = serverRepository;
        }


        [FunctionName(nameof(Packages))]
        [OpenApiOperation(operationId: "Packages", tags: new[] { "semVerLevel" })]
        [OpenApiParameter(name: "semVerLevel", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **semVerLevel** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Packages(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Packages")] HttpRequest req, CancellationToken token = default)
        {
            Logger.LogInformation("C# HTTP trigger function processed a request.");

            var clientCompatibility = SemanticVersion.TryParse(req.Query["semVerLevel"], out var parseOutput) ? new ClientCompatibility(parseOutput) : ClientCompatibility.Default;
            var result = await ServerRepository.GetPackagesAsync(clientCompatibility, token);
            return new OkObjectResult(result);
        }

        [FunctionName(nameof(PackagesByIdAndVersion))]
        [OpenApiOperation(operationId: "Packages", tags: new[] { "id", "version" })]
        [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = false, Type = typeof(string), Description = "The **id** parameter")]
        [OpenApiParameter(name: "version", In = ParameterLocation.Path, Required = false, Type = typeof(string), Description = "The **version** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> PackagesByIdAndVersion(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Packages(Id={id:alpha},Version={version:alpha})")] HttpRequest req,
            string id,
            string version,
            CancellationToken token)
        {
            var package = SemanticVersion.TryParse(version, out var semVer) 
                ? await ServerRepository.FindPackageAsync(id, new SemanticVersion(semVer), token) 
                : await ServerRepository.FindPackageAsync(id, ClientCompatibility.Max, token);
            if (package == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(package);
        }
    }
}

