using Microsoft.AspNetCore.Http;
using NuGet.Service.Core;

namespace NuGet.Functions.Extensions;
internal static class QueryCollectionExtensions
{
    internal static SecretsTokens GetSecretsTokens(this IQueryCollection query)
    {
        var secretsTokens = new SecretsTokens();
        if (query.TryGetValue(nameof(secretsTokens.q), out var q))
        {
            secretsTokens.q = q;
        }
        if (query.TryGetValue(nameof(secretsTokens.skip), out var outputString) && int.TryParse(outputString, out int skip))
        {
            secretsTokens.skip = skip;
        }
        if (query.TryGetValue(nameof(secretsTokens.skip), out outputString) && int.TryParse(outputString, out int take))
        {
            secretsTokens.take = take;
        }
        if (query.TryGetValue(nameof(secretsTokens.prerelease), out outputString) && bool.TryParse(outputString, out bool prerelease))
        {
            secretsTokens.prerelease = prerelease;
        }
        if (query.TryGetValue(nameof(secretsTokens.semVerLevel), out outputString) && bool.TryParse(outputString, out bool semVerLevel))
        {
            secretsTokens.semVerLevel = semVerLevel;
        }
        if (query.TryGetValue(nameof(secretsTokens.packageType), out outputString) && bool.TryParse(outputString, out bool packageType))
        {
            secretsTokens.packageType = packageType;
        }
        return secretsTokens;
    }
}
