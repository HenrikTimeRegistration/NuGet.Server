using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using NuGet.Functions.Options;
using NuGet.Service.Core.Result;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NuGet.Functions
{
    public class Entry
    {
        public Entry(UploadLocation uploadLocation)
        {
            UploadLocation = uploadLocation;
        }

        private UploadLocation UploadLocation { get; init; }

        [Function(nameof(GetEntryIndex))]
        [OpenApiOperation(operationId: "Entry")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(ApiIndex), Description = "The OK response")]
        public async Task<IActionResult> GetEntryIndex([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "index.josn")] HttpRequest req, CancellationToken token = default)
        {
            var baseUrl = new Uri($"{req.Scheme}://{req.Host}");
            var index = new ApiIndex() { version = "3.0.0" };

            index.resources.Add(new() { Id = Path.Combine(baseUrl.AbsoluteUri, "v3-Packages"), Type = "PackageBaseAddress/3.0.0" });
            index.resources.Add(new() { Id = Path.Combine(UploadLocation.NuGetApiIndexPath, "v2/package"), Type = "PackagePublish/2.0.0" });
            //index.resources.Add(new() { Id = $"{baseUrl.AbsoluteUri}/PackageMetadata", Type = "RegistrationsBaseUrl/3.0.0-rc" });
            index.resources.Add(new() { Id = Path.Combine(baseUrl.AbsoluteUri, "query"), Type = "SearchQueryService/3.5.0" });
            return new OkObjectResult(index);
        }
    }
}
