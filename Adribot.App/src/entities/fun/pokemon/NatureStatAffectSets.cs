using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class NatureStatAffectSets
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // A list of natures and how they change the referenced stat.
    [JsonPropertyName("increase")]
    public List<NamedApiResource> Increase { get; set; }

    // A list of natures and how they change the referenced stat.
    [JsonPropertyName("decrease")]
    public List<NamedApiResource> Decrease { get; set; }
}
