using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokemonAbility(
    [property: JsonPropertyName("is_hidden")] bool IsHidden,

    // The slot this ability occupies in this Pokémon species.
    [property: JsonPropertyName("slot")] int Slot,
    [property: JsonPropertyName("ability")] NamedApiResource Ability
);
