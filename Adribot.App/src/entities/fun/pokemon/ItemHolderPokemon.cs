using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class ItemHolderPokemon
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("pokemon")]
    public NamedApiResource Pokemon { get; set; }

    // The details for the version that this item is held in by the Pokémon.
    [JsonPropertyName("version_details")]
    public List<ItemHolderPokemonVersionDetail> VersionDetails { get; set; }
}
