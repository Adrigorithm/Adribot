using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonEntry
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("entry_number")]
    public int EntryNumber { get; set; }

    [JsonPropertyName("pokemon_species")]
    public NamedApiResource PokemonSpecies { get; set; }
}
