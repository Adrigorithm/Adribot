using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokeathlonStat(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // A detail of natures which affect this Pokéathlon stat positively or negatively.
    [property: JsonPropertyName("affecting_natures")] NaturePokeathlonStatAffectSets AffectingNatures
);
