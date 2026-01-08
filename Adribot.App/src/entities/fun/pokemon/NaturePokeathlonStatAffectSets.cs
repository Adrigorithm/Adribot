using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class NaturePokeathlonStatAffectSets
{
    // A list of natures and how they change the referenced Pokéathlon stat.
    [JsonPropertyName("increase")]
    public List<NaturePokeathlonStatAffect> Increase { get; set; }

    // A list of natures and how they change the referenced Pokéathlon stat.
    [JsonPropertyName("decrease")]
    public List<NaturePokeathlonStatAffect> Decrease { get; set; }
}
