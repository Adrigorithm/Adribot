using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveLearnMethod
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The description of this resource listed in different languages.
    [JsonPropertyName("descriptions")]
    public List<Description> Descriptions { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // A list of version groups where moves can be learned through this method.
    [JsonPropertyName("version_groups")]
    public List<NamedApiResource> VersionGroups { get; set; }
}
