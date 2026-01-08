using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class SuperContestEffect
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The level of appeal this super contest effect has.
    [JsonPropertyName("appeal")]
    public int Appeal { get; set; }

    // The flavour text of this super contest effect listed in different languages.
    [JsonPropertyName("flavor_text_entries")]
    public List<FlavourText> FlavourTextEntries { get; set; }

    // A list of moves that have the effect when used in super contests.
    [JsonPropertyName("moves")]
    public List<NamedApiResource> Moves { get; set; }
}
