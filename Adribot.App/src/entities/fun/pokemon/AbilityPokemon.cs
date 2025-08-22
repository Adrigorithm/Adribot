using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record AbilityPokemon(
    [property: JsonPropertyName("is_hidden")] bool IsHidden,

    // Pokémon have 3 ability 'slots' which hold references to possible abilities they could have.
    // This is the slot of this ability for the referenced pokemon.
    [property: JsonPropertyName("slot")] int Slot,
    [property: JsonPropertyName("pokemon")] NamedApiResource Pokemon
);
