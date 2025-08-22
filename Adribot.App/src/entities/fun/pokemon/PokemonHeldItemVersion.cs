using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokemonHeldItemVersion(
    [property: JsonPropertyName("version")] NamedApiResource Version,
    [property: JsonPropertyName("rarity")] int Rarity
);
