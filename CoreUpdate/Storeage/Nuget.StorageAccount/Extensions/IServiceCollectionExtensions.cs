using Microsoft.Extensions.DependencyInjection;
using NuGet.Service.Core.Interfaces.Storage;

namespace Nuget.StorageAccount.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddScoped<INugetPackageCRUD, PackageCRUD>();
        return services;
    }
}
