using NuGet.Frameworks;
using NuGet.Packaging.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service.Core;
public class PackageDependencySet : IFrameworkTargetable
{
    public PackageDependencySet() { }

    public PackageDependencySet(FrameworkName targetFramework, IEnumerable<PackageDependency> dependencies)
    {
        ArgumentNullException.ThrowIfNull(dependencies, nameof(dependencies));

        TargetFramework = targetFramework;
        Dependencies = new ReadOnlyCollection<PackageDependency>(dependencies.ToList());
    }

    public required FrameworkName TargetFramework { get; init; }

    public IReadOnlyCollection<PackageDependency> Dependencies { get; init; } = new List<PackageDependency>();

    public IEnumerable<FrameworkName> SupportedFrameworks
    {
        get
        {
            if (!(TargetFramework == null))
            {
                yield return TargetFramework;
            }
        }
    }

    IEnumerable<NuGetFramework> IFrameworkTargetable.SupportedFrameworks => throw new NotImplementedException();

}
