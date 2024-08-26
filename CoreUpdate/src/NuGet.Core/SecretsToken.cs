namespace NuGet.Service.Core;
public class SecretsTokens
{
    public string q { get; set; } = string.Empty;

    public int skip { get; set; } = 0;

    public int take { get; set; } = int.MaxValue;

    public bool prerelease { get; set; } = false;

    public bool semVerLevel { get; set; } = false;

    public bool packageType { get; set; } = false;
}
