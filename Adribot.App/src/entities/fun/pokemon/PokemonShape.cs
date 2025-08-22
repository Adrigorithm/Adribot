using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record PokemonShape(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,

    // The "scientific" name of this Pokémon shape listed in different languages.
    [property: JsonPropertyName("awesome_names")] List<AwesomeName> AwesomeNames,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // A list of the Pokémon species that have this shape.
    [property: JsonPropertyName("pokemon_species")] List<NamedApiResource> PokemonSpecies
);
