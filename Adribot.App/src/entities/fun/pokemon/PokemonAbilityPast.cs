using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonAbilityPast(
    [property: JsonPropertyName("generation")] NamedAPIResource Generation,

    [property: JsonPropertyName("abilities")] List<PokemonAbility> Abilities
);
