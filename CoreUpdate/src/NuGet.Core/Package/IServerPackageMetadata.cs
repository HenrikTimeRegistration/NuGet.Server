using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service.Core.Package;
public interface IServerPackageMetadata
{
    Uri ReportAbuseUrl { get; }

    int DownloadCount { get; }
}
