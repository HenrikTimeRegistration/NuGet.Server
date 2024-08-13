using NuGet.Packaging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service.Core.Package;
public interface IPackage : IPackageMetadata, IPackageName, IServerPackageMetadata
{
    bool IsAbsoluteLatestVersion { get; }

    bool IsLatestVersion { get; }

    bool Listed { get; }

    DateTimeOffset? Published { get; }

    IEnumerable<IPackageAssemblyReference> AssemblyReferences { get; }
}
