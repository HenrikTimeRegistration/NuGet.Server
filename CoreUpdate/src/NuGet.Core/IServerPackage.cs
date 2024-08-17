using NuGet.Service.Core.Package;
using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service.Core;
public interface IServerPackage : IPackageName
{
    string Title { get; }
    IEnumerable<string> Authors { get; }
    IEnumerable<string> Owners { get; }
    Uri IconUrl { get; }
    Uri LicenseUrl { get; }
    Uri ProjectUrl { get; }
    int DownloadCount { get; }
    bool RequireLicenseAcceptance { get; }
    bool DevelopmentDependency { get; }
    string Description { get; }
    string Summary { get; }
    string ReleaseNotes { get; }
    IEnumerable<PackageDependencySet> DependencySets { get; }
    string Copyright { get; }
    string Tags { get; }
    bool SemVer1IsAbsoluteLatest { get; }
    bool SemVer1IsLatest { get; }
    bool SemVer2IsAbsoluteLatest { get; }
    bool SemVer2IsLatest { get; }
    bool Listed { get; }
    Version MinClientVersion { get; }
    string Language { get; }
    long PackageSize { get; }
    string PackageHash { get; }
    string PackageHashAlgorithm { get; }
    string FullPath { get; }
    DateTimeOffset LastUpdated { get; }
    DateTimeOffset Created { get; }

    IEnumerable<FrameworkName> GetSupportedFrameworks();
}
