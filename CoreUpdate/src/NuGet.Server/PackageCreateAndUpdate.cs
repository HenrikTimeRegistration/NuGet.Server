﻿using NuGet.Packaging;
using NuGet.Service.Core.Interfaces.Logic;
using NuGet.Service.Core.Interfaces.Storage;
using NuGet.Service.Core.ResoultObject;
using System.IO;
using System.IO.Compression;
using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NuGet.Service;
public class PackageCreateAndUpdate : IPackageCreateAndUpdate
{
    public PackageCreateAndUpdate (INugetPackageCRUD storeage)
    {
        Storeage = storeage;
    }

    INugetPackageCRUD Storeage { get; set; }

    public Task DeletePackageAsync(NuGetIdentity nuGetIdentity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RelistPackageAsync(NuGetIdentity nuGetIdentity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UploadPackageAsync(Stream nugkg, CancellationToken cancellationToken = default)
    {
        using var Package = new PackageArchiveReader(nugkg);
        var packageIdentity = await Package.GetIdentityAsync(cancellationToken);
        var version = packageIdentity.Version.OriginalVersion ?? packageIdentity.Version.ToString() ?? throw new NullReferenceException();        
        await Storeage.AddNugetPackageAsync(nugkg, new NuGetIdentity() { Id = packageIdentity.Id, Version = version });
    }
}
