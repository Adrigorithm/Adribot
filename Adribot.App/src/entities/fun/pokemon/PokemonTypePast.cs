using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonTypePast(
    [property: JsonPropertyName("generation")] NamedApiResource Generation,

    [property: JsonPropertyName("types")] List<PokemonType> Types
);
