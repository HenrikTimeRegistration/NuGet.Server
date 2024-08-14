using NuGet.Frameworks;
using NuGet.Packaging;

namespace NuGet.Service.Core.Package;
public interface IPackageAssemblyReference : IPackageFile, IFrameworkTargetable
{
    string Name { get; }
}
