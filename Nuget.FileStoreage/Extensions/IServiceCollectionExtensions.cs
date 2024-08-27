using Microsoft.Extensions.DependencyInjection;
using NuGet.Service.Core;

namespace Nuget.StorageAccount.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddScoped<INugetPackageCRUD, PackageCRUD>();
        return services;
    }
}
