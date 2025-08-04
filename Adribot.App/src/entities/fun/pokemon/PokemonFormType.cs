using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonFormType(
    // The order the Pokémon's types are listed in.
    [property: JsonPropertyName("slot")] int Slot,

    [property: JsonPropertyName("type")] NamedApiResource Type
);
