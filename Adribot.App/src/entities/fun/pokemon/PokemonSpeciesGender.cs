using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonSpeciesGender(
    // The chance of this Pokémon being female, in eighths; or -1 for genderless.
    [property: JsonPropertyName("rate")] int Rate,
    
    [property: JsonPropertyName("pokemon_species")] NamedApiResource PokemonSpecies
);
