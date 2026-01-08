using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonSpeciesDexEntry
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The index number within the Pokédex.
    [JsonPropertyName("entry_number")]
    public int EntryNumber { get; set; }

    // The Pokédex the referenced Pokémon species can be found in.
    [JsonPropertyName("pokedex")]
    public NamedApiResource Pokedex { get; set; }
}
