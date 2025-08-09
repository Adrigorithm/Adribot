using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record Generation(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("abilities")] List<NamedApiResource> Abilities,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // The main region travelled in this generation.
    [property: JsonPropertyName("main_region")] NamedApiResource MainRegion,

    [property: JsonPropertyName("moves")] List<NamedApiResource> Moves,
    [property: JsonPropertyName("pokemon_species")] List<NamedApiResource> PokemonSpecies,
    [property: JsonPropertyName("types")] List<NamedApiResource> Types,

    // A list of version groups that were introduced in this generation.
    [property: JsonPropertyName("version_groups")] List<NamedApiResource> VersionGroups
);
