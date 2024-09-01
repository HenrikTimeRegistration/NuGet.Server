using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Nuget.FileStorage.Data;
using NuGet.Service.Core;

namespace Nuget.FileStorage.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddFileStorage(this IServiceCollection serviceCollection, string ConfigName)
    {
        serviceCollection.AddSingleton<DirectoryOptions>( x => 
            x.GetRequiredService<IConfiguration>().GetRequiredSection(ConfigName).Get<DirectoryOptions>() ?? 
            throw new NullReferenceException());

        serviceCollection.AddScoped<INugetPackageCRUD, PackageCRUD>();
    }
}
