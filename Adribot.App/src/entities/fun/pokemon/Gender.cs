using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public class Gender
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    // A list of Pokémon species that can be this gender and how likely it is that they will be.
    [JsonPropertyName("pokemon_species_details")]
    public List<PokemonSpeciesGender> PokemonSpeciesDetails { get; set; }

    // A list of Pokémon species that required this gender in order for a Pokémon to evolve into them.
    [JsonPropertyName("required_for_evolution")]
    public List<NamedApiResource> RequiredForEvolution { get; set; }
}
