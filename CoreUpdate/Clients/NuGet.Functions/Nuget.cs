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


        [FunctionName("Packages")]
        [OpenApiOperation(operationId: "Packages", tags: new[] { "semVerLevel" })]
        [OpenApiParameter(name: "semVerLevel", In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The **semVerLevel** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Packages(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, CancellationToken token = default(CancellationToken))
        {
            Logger.LogInformation("C# HTTP trigger function processed a request.");

            var clientCompatibility = SemanticVersion.TryParse(req.Query["semVerLevel"], out var parseOutput) ? new ClientCompatibility(parseOutput) : ClientCompatibility.Default;
            var resoult = await ServerRepository.GetPackagesAsync(clientCompatibility, token);
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            return new OkObjectResult("Hello, World!");
        }
    }
}

