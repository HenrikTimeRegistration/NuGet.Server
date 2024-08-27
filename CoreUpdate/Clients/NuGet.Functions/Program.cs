using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nuget.FileStorage.Extensions;
using NuGet.Service;
using NuGet.Service.Core;

namespace NuGet.Functions;

public static class Program
{
    public static void Main(string[] args)
    {
        IHost host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults()
            .ConfigureServices((services) =>
            {
                services.AddFileStorage("FileStorage");
                services.AddScoped<IPackageCreateAndUpdate, PackageCreateAndUpdate>();
            })
            .Build();

        host.Run();
    }
}