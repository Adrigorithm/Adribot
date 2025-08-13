using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PokemonHabitat(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    
    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,
    
    // A list of the Pokémon species that can be found in this habitat.
    [property: JsonPropertyName("pokemon_species")] List<NamedApiResource> PokemonSpecies
);
