using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Version
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    [JsonPropertyName("version_group")]
    public NamedApiResource VersionGroup { get; set; }
}
