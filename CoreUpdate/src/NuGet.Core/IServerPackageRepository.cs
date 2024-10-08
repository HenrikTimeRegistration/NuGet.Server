﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using NuGet.Service.Core;
using NuGet.Service.Core.Package;
using NuGet.Versioning;

namespace NuGet.Service.Core
{
    public interface IServerPackageRepository
    {
        string Source { get; }

        Task AddPackageAsync(IPackage package, CancellationToken token);

        Task<IEnumerable<IServerPackage>> GetPackagesAsync(ClientCompatibility compatibility, CancellationToken token);

        Task<IEnumerable<IServerPackage>> SearchAsync(
            string searchTerm,
            IEnumerable<string> targetFrameworks,
            bool allowPrereleaseVersions,
            ClientCompatibility compatibility,
            CancellationToken token);

        Task<IEnumerable<IServerPackage>> SearchAsync(
            string searchTerm,
            IEnumerable<string> targetFrameworks,
            bool allowPrereleaseVersions,
            bool allowUnlistedVersions,
            ClientCompatibility compatibility,
            CancellationToken token);

        Task ClearCacheAsync(CancellationToken token);

        Task RemovePackageAsync(string packageId, SemanticVersion version, CancellationToken token);

        Task<IServerPackage> FindPackageAsync(string packageId, SemanticVersion version, CancellationToken token);
        Task<IServerPackage> FindPackageAsync(string packageId, ClientCompatibility version, CancellationToken token);
    }
}
