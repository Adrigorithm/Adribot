using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record ItemHolderPokemonVersionDetail(
    // How often this Pokémon holds this item in this version.
    [property: JsonPropertyName("rarity")] int Rarity,
    [property: JsonPropertyName("version")] NamedApiResource Version
);
