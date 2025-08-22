using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Gender(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // A list of Pokémon species that can be this gender and how likely it is that they will be.
    [property: JsonPropertyName("pokemon_species_details")] List<PokemonSpeciesGender> PokemonSpeciesDetails,

    // A list of Pokémon species that required this gender in order for a Pokémon to evolve into them.
    [property: JsonPropertyName("required_for_evolution")] List<NamedApiResource> RequiredForEvolution
);
