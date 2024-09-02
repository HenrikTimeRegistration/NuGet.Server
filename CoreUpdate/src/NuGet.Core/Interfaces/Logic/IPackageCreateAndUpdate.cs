using NuGet.Packaging.Core;
using NuGet.Service.Core.ResoultObject;

namespace NuGet.Service.Core.Interfaces.Logic;

public interface IPackageCreateAndUpdate
{
    public Task UploadPackageAsync(Stream nupkg, CancellationToken cancellationToken = default);

    public Task DeletePackageAsync(NuGetIdentity packageIdentity, CancellationToken cancellationToken = default);

    public Task RelistPackageAsync(NuGetIdentity packageIdentity, CancellationToken cancellationToken = default);
}
