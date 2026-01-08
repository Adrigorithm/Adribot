using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveDamageClass
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The description of this resource listed in different languages.
    [JsonPropertyName("descriptions")]
    public List<Description> Descriptions { get; set; }

    // A list of moves that fall into this damage class.
    [JsonPropertyName("moves")]
    public List<NamedApiResource> Moves { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }
}
