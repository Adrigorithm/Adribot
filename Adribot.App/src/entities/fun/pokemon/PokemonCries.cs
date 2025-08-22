using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokemonCries(
    [property: JsonPropertyName("latest")] string Latest,
    [property: JsonPropertyName("legacy")] string Legacy
);
