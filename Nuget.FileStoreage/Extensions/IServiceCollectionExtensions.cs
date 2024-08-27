using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Nuget.FileStorage.Data;
using NuGet.Service.Core;

namespace Nuget.FileStorage.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddFileStorage(this IServiceCollection serviceCollection, string ConfigName)
    {
        serviceCollection.AddOptions<DirectoryOptions>()
            .Configure<IConfiguration>((directoryOptions, configuration) =>
            {
                configuration
                    .GetSection(ConfigName)
                    .Bind(directoryOptions);
            });

        serviceCollection.AddScoped<INugetPackageCRUD, PackageCRUD>();
    }
}
