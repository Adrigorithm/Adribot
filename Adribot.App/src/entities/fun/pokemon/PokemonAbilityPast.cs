using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonAbilityPast(
    [property: JsonPropertyName("generation")] NamedApiResource Generation,
    [property: JsonPropertyName("abilities")] List<PokemonAbility> Abilities
);
