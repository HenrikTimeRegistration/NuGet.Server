namespace NuGet.Service.Core.ResoultObject;
public class NuGetIdentity
{
    private string id;

    private string version = string.Empty;

    public required string Id { get => id; set => id = value.ToLower(); }
    public string Version { get => version; set => version = value.ToLower(); }
}
