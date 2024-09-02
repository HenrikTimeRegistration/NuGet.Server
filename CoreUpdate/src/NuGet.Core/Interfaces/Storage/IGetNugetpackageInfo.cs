using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service.Core.Interfaces.Storage;
public interface IGetNuGetPackageInfo
{
    public Task<NuGetPackageData> GetListOfPackagesAsync(string id);
}
