using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nuget.FileStorage.Extensions;
using NuGet.Service;
using NuGet.Service.Core;
using Microsoft.Azure.WebJobs.Extensions.OpenApi;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;
using System;

namespace NuGet.Functions;

public static class Program
{
    public static void Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWebApplication(builder =>
            {
                builder.Services.Configure<KestrelServerOptions>(options =>
                {
                    options.AllowSynchronousIO = true;
                    options.Limits.MaxRequestBodySize = null; // Set the limit to 100 MB
                });
                builder.Services.Configure<FormOptions>(options =>
                {
                    options.ValueLengthLimit = int.MaxValue;
                    options.MultipartBodyLengthLimit = int.MaxValue;
                    options.MultipartHeadersLengthLimit = int.MaxValue;
                });
            })
            .ConfigureServices((services) =>
            {
                services.AddLogging();
                services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
                {
                    var options = new OpenApiConfigurationOptions()
                    {
                        Servers = new List<OpenApiServer>() { new() { Url = "/api/" } },
                        OpenApiVersion = OpenApiVersionType.V3,
                        IncludeRequestingHostName = false,
                    };

                    return options;
                });
                services.AddFileStorage("FileStorage");
                services.AddScoped<IPackageCreateAndUpdate, PackageCreateAndUpdate>();
            })
            .Build();

        host.Run();
    }
}