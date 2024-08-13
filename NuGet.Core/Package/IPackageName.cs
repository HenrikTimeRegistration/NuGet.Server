using NuGet.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service.Core.Package;
public interface IPackageName
{
    string Id { get; }
    SemanticVersion Version { get; }
}
