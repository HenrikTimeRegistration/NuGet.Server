using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using NuGet.Service.Core.Result;
using System;
using System.Net;

namespace NuGet.Functions
{
    public class Entry
    {
        [Function(nameof(GetEntryIndex))]
        [OpenApiOperation(operationId: "Entry")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(ApiIndex), Description = "The OK response")]
        public IActionResult GetEntryIndex([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "index.josn")] HttpRequest req)
        {
            var url = new Uri(req.PathBase);
            var baseUrl = new Uri($"{url.Scheme}://{url.Authority}");
            var index = new ApiIndex() { version = "3.0.0" };

            index.resources.Add(new() { Id = $"{baseUrl.AbsoluteUri}/v3-Packages", Type = "PackageBaseAddress/3.0.0" });
            index.resources.Add(new() { Id = $"{baseUrl.AbsoluteUri}/v2/package", Type = "PackagePublish/2.0.0" });
            //index.resources.Add(new() { Id = $"{baseUrl.AbsoluteUri}/PackageMetadata", Type = "RegistrationsBaseUrl/3.0.0-rc" });
            index.resources.Add(new() { Id = $"{baseUrl.AbsoluteUri}/query", Type = "SearchQueryService/3.5.0" });
            return new OkObjectResult(index);
        }
    }
}
