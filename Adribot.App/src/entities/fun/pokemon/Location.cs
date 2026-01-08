using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Location
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("region")]
    public NamedApiResource Region { get; set; }

    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    [JsonPropertyName("game_indices")]
    public List<GenerationGameIndex> GameIndices { get; set; }

    [JsonPropertyName("areas")]
    public List<NamedApiResource> Areas { get; set; }
}
