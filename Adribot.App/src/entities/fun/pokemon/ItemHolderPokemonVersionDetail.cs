using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class ItemHolderPokemonVersionDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // How often this Pokémon holds this item in this version.
    [JsonPropertyName("rarity")]
    public int Rarity { get; set; }

    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }
}
