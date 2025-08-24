using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.entities.fun.pokemon;

public record Region(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("locations")] List<NamedApiResource> Locations,
    [property: JsonPropertyName("name")] string Name,

    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,

    // The generation this region was introduced in.
    [property: JsonPropertyName("main_generation")] NamedApiResource MainGeneration,

    // A list of pokédexes that catalogue Pokémon in this region.
    [property: JsonPropertyName("pokedexes")] List<NamedApiResource> Pokedexes,

    // A list of version groups where this region can be visited.
    [property: JsonPropertyName("version_groups")] List<NamedApiResource> VersionGroups
);
