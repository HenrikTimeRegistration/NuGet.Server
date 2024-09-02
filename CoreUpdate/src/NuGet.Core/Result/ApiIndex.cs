using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service.Core.Result;
public class ApiIndex
{
    public required string version { get; set; }

    public List<ApiResources> resources { get; set; } = new();
}
