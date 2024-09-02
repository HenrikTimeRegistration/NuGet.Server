using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NuGet.Service.Core.Result;

namespace NuGet.WebApp.Controllers;
public class EntryController : ControllerBase
{
    [Route("index.josn")]
    [HttpGet]
    public async Task<IActionResult> GetEntryIndex(CancellationToken token = default)
    {
        var url = new Uri(Request.GetDisplayUrl());
        var baseUrl = new Uri($"{url.Scheme}://{url.Authority}");
        var index = new ApiIndex() { version = "3.0.0" };

        index.resources.Add(new() { Id = $"{baseUrl.AbsoluteUri}/v3-Packages", Type = "PackageBaseAddress/3.0.0" });
        index.resources.Add(new() { Id = $"{baseUrl.AbsoluteUri}/v2/package", Type = "PackagePublish/2.0.0" });
        //index.resources.Add(new() { Id = $"{baseUrl.AbsoluteUri}/PackageMetadata", Type = "RegistrationsBaseUrl/3.0.0-rc" });
        index.resources.Add(new() { Id = $"{baseUrl.AbsoluteUri}/query", Type = "SearchQueryService/3.5.0" });
        return new OkObjectResult(index);
    }
}
