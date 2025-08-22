using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record TypePokemon(
    // The order the Pokémon's types are listed in.
    [property: JsonPropertyName("slot")] int Slot,
    [property: JsonPropertyName("pokemon")] NamedApiResource Pokemon
);
