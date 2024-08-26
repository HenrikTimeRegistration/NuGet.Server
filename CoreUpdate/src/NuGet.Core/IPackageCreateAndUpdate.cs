namespace NuGet.Service.Core;

public interface IPackageCreateAndUpdate
{
    public Task UploadPackageAsync(Stream stream);

    public Task DeletePackageAsync(string packageName, string packageVersion);

    public Task RelistPackageAsync(string packageName, string packageVersion);
}
