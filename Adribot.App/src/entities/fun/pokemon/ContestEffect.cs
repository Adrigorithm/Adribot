using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ContestEffect
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The base number of hearts the user of this move gets.
    [JsonPropertyName("appeal")]
    public int Appeal { get; set; }

    // The base number of hearts the user's opponent loses.
    [JsonPropertyName("jam")]
    public int Jam { get; set; }

    // The result of this contest effect listed in different languages.
    [JsonPropertyName("effect_entries")]
    public List<Effect> EffectEntries { get; set; }

    // The flavour text of this contest effect listed in different languages.
    [JsonPropertyName("flavor_text_entries")]
    public List<FlavourText> FlavourTextEntries { get; set; }
}
