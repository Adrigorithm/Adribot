using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokemonColour(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // A list of the Pokémon species that have this color.
    [property: JsonPropertyName("pokemon_species")] List<NamedApiResource> PokemonSpecies
);
