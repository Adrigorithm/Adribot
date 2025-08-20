using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonSpeciesVariety(
    // Whether this variety is the default variety.
    [property: JsonPropertyName("is_default")] bool IsDefault,
    [property: JsonPropertyName("pokemon")] NamedApiResource Pokemon
);
