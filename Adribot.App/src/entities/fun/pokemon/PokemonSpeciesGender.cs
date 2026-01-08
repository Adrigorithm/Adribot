using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PokemonSpeciesGender
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The chance of this Pokémon being female, in eighths; or -1 for genderless.
    [JsonPropertyName("rate")]
    public int Rate { get; set; }

    [JsonPropertyName("pokemon_species")]
    public NamedApiResource PokemonSpecies { get; set; }
}
