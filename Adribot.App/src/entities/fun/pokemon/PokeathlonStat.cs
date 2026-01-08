using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokeathlonStat
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // The name of this resource listed in different languages.
    [JsonPropertyName("names")]
    public List<Name> Names { get; set; }

    // A detail of natures which affect this Pokéathlon stat positively or negatively.
    [JsonPropertyName("affecting_natures")]
    public NaturePokeathlonStatAffectSets AffectingNatures { get; set; }
}
