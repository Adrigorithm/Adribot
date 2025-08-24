using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ContestEffect(
    [property: JsonPropertyName("id")] int Id,

    // The base number of hearts the user of this move gets.
    [property: JsonPropertyName("appeal")] int Appeal,

    // The base number of hearts the user's opponent loses.
    [property: JsonPropertyName("jam")] int Jam,

    // The result of this contest effect listed in different languages.
    [property: JsonPropertyName("effect_entries")] List<Effect> EffectEntries,

    // The flavour text of this contest effect listed in different languages.
    [property: JsonPropertyName("flavor_text_entries")] List<FlavourText> FlavourTextEntries
);
