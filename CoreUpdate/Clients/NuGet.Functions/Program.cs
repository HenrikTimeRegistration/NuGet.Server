using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nuget.FileStorage.Extensions;
using NuGet.Service;
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
using NuGet.Service.Core.Interfaces.Logic;
using Microsoft.Extensions.Configuration;
using Nuget.FileStorage.Data;
using NuGet.Functions.Options;

namespace NuGet.Functions;

public static class Program
{
    public static void Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWebApplication(x =>
            {
                x.Services.AddCors(options =>
                {
                    options.AddPolicy(name: "MyPolicy",
                        policy =>
                        {
                            policy.WithOrigins("https://localhost:7130")
                                .WithMethods("PUT");
                        });
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
                services.AddScoped<IPackageData, PackageData>();
                services.AddSingleton<UploadLocation>(x =>
                    x.GetRequiredService<IConfiguration>().GetSection("UpladLocation")?.Get<UploadLocation>());
                services.AddHttpClient();
            })
            .Build();

        host.Run();
    }
}