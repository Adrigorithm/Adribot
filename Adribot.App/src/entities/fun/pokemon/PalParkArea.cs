using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Adribot.Entities.fun.pokemon;

public record PalParkArea(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    
    // The name of this resource listed in different languages.
    [property: JsonPropertyName("names")] List<Name> Names,
    
    [property: JsonPropertyName("pokemon_encounters")] List<PalParkEncounterSpecies> PokemonEncounters
);
