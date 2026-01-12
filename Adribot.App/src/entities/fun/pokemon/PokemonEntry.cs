using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Adribot.entities.fun.pokemon;

public class PokemonEntry
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("entry_number")]
    public int EntryNumber { get; set; }

    [DeleteBehavior(DeleteBehavior.Restrict)]
    [JsonPropertyName("pokemon_species")]
    public NamedApiResource PokemonSpecies { get; set; }
}
