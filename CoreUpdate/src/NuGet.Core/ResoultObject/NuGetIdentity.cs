namespace NuGet.Service.Core.ResoultObject;
public class NuGetIdentity
{
    private string id;

    private string version;

    public required string Id { get => id; set => id = value.ToLower(); }
    public required string Version { get => version; set => version = value.ToLower(); }
}
