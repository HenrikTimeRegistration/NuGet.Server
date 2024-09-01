﻿namespace NuGet.Service.Core;

public interface IPackageCreateAndUpdate
{
    public Task UploadPackageAsync(Stream nupkg, CancellationToken cancellationToken = default);

    public Task DeletePackageAsync(string packageName, string packageVersion, CancellationToken cancellationToken = default);

    public Task RelistPackageAsync(string packageName, string packageVersion, CancellationToken cancellationToken = default);
}
