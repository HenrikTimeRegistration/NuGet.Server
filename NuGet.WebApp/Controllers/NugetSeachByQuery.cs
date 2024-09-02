using Microsoft.AspNetCore.Mvc;
using NuGet.Service.Core.Extensions;

namespace NuGet.WebApp.Controllers;

public class NugetSeachByQuery : ControllerBase
{
    private readonly ILogger<NugetSeachByQuery> _logger;

    public NugetSeachByQuery(ILogger<NugetSeachByQuery> log)
    {
        _logger = log;
    }

    [Route("query")]
    [HttpGet]
    public async Task<IActionResult> GetPackagesBuQuerySeach(HttpRequest req,
        CancellationToken token = default)
    {
        var secretsTokens = req.Query.GetSecretsTokens();
        return new OkResult();
    }
}

