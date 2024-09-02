using NuGet.Service.Core.ResoultObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service.Core;
public class NuGetPackageData
{
    public required string Name { get; set; }

    public required IEnumerable<string> Versions { get; set; }

}
