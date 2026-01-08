using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class PalParkEncounterSpecies
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    // The base score given to the player when this Pokémon is caught during a pal park run.
    [JsonPropertyName("base_score")]
    public int BaseScore { get; set; }

    // The base rate for encountering this Pokémon in the pal park area.
    [JsonPropertyName("rate")]
    public int Rate { get; set; }

    [JsonPropertyName("pokemon_species")]
    public NamedApiResource PokemonSpecies { get; set; }
}
