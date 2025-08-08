using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record VersionGroup(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    
    // Order for sorting. Almost by date of release, except similar versions are grouped together.
    [property: JsonPropertyName("order")] int Order,
    
    [property: JsonPropertyName("generation")] NamedApiResource Generation,
    
    // A list of methods in which Pokémon can learn moves in this version group.
    [property: JsonPropertyName("move_learn_methods")] List<NamedApiResource> MoveLearnMethods,
    
    [property: JsonPropertyName("pokedexes")] List<NamedApiResource> Pokedexes,
    
    // A list of regions that can be visited in this version group.
    [property: JsonPropertyName("regions")] List<NamedApiResource> Regions,
    
    [property: JsonPropertyName("versions")] List<NamedApiResource> Versions
);
