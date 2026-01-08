using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class MoveTarget
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The description of this resource listed in different languages.
    [JsonPropertyName("descriptions")]
    public List<Description> Descriptions { get; set; }

    // A list of moves that that are directed at this target.
    [JsonPropertyName("moves")]
    public List<NamedApiResource> Moves { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }
}
