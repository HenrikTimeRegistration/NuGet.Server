using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nuget.FileStorage.Data;

public class DirectoryOptions
{
    public const string Package = "nugetPackage"; 

    public required string BaseFilePath { get; init; }

    public required string Prefix { get; init; } = string.Empty;
}
