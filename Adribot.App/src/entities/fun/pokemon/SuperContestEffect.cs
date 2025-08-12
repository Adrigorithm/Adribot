using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record SuperContestEffect(
    [property: JsonPropertyName("id")] int Id,

    // The level of appeal this super contest effect has.
    [property: JsonPropertyName("appeal")] int Appeal,

    // The flavor text of this super contest effect listed in different languages.
    [property: JsonPropertyName("flavor_text_entries")] List<FlavourText> FlavourTextEntries,
    
    // A list of moves that have the effect when used in super contests.
    [property: JsonPropertyName("moves")] List<NamedApiResource> Moves
);
