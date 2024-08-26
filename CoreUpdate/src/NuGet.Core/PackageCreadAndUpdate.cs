using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGet.Service.Core;
public interface PackageCreateAndUpdate
{
    public Task UploadPackageAsync(Stream stream);

    public Task DeletePackageAsync(string packageName, string packageVersion);

    public Task RelistPackageAsync(string packageName, string packageVersion);
}
