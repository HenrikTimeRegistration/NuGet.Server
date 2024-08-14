using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NuGet.Service.Core;

namespace NuGet.Functions
{
    public class Nuget
    {
        private readonly ILogger<Nuget> _logger;

        public Nuget(ILogger<Nuget> log, IServerPackageRepository serverRepository)
        {
            _logger = log;
            ServerRepository = serverRepository;
        }

        private required IServerPackageRepository ServerRepository { get; }

        [FunctionName("Packages")]
        [OpenApiOperation(operationId: "Packages")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Packages(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];
            ServerRepository.GetPackagesAsync();
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            return new OkObjectResult("Hello, World!");
        }
    }
}

