using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Net;

namespace NuGet.WebApp.Controllers;

public class NugetPackageMetadata : ControllerBase
{
    private readonly ILogger<NugetPackageMetadata> _logger;

    public NugetPackageMetadata(ILogger<NugetPackageMetadata> log)
    {
        _logger = log;
    }

    [Route("PackageMetadata/{id}/index.josn")]
    [HttpGet]
    public async Task<IActionResult> GetPackageMetadata(string id, CancellationToken token = default)
    {
        id = id.ToLower();
        await Task.CompletedTask;
        return new OkResult();
    }
}

