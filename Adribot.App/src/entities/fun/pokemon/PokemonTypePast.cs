using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonTypePast(
    [property: JsonPropertyName("generation")] NamedAPIResource Generation,

    [property: JsonPropertyName("types")] List<PokemonType> Types
);
