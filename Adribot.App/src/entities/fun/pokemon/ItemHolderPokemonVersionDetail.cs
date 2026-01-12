using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class ItemHolderPokemonVersionDetail
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // How often this Pokémon holds this item in this version.
    [JsonPropertyName("rarity")]
    public int Rarity { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("version")]
    public NamedApiResource Version { get; set; }
}
