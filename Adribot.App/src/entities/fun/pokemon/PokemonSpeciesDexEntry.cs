using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonSpeciesDexEntry
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The index number within the Pokédex.
    [JsonPropertyName("entry_number")]
    public int EntryNumber { get; set; }

    // The Pokédex the referenced Pokémon species can be found in.
    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("pokedex")]
    public NamedApiResource Pokedex { get; set; }
}
