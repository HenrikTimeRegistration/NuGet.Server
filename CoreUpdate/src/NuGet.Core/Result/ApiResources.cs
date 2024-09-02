using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NuGet.Service.Core.Result;
public class ApiResources
{
    [JsonProperty(PropertyName = "@id")]
    public required string Id { get; set; }

    [JsonProperty(PropertyName = "@type")]
    public required string Type { get; set; }
}

public class ApiResourcesWithComment : ApiResources
{
    public string comment { get; set; } = string.Empty;
}
